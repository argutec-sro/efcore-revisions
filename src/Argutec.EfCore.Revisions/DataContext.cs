using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Argutec.EfCore.Revisions
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions aOptions) : base(aOptions)
        {

        }

        public DbSet<Revision> Revisions { get; set; }
        public override int SaveChanges()
        {
            SaveRevision();
            return base.SaveChanges();
        }
        public int SaveChangesWithoutRevisions()
        {
            return base.SaveChanges();
        }

        public void SaveRevision()
        {
            Guid lBatchID = Guid.NewGuid();
            // TODO: Logged User
            string lUserID = "TEST";
            List<Revision> lNewRevisions = new List<Revision>();

            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Modified))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);

                var lMembers = nRecord.Properties.Where(aR => aR.IsModified);
                foreach (var nColumn in lMembers)
                {
                    lNewRevisions.Add(new Revision
                    {
                        ID = Guid.NewGuid(),
                        RecordID = lPrimaryKey,
                        Table = lTableName,
                        Column = nColumn.Metadata.Name,
                        Original = nColumn.OriginalValue?.ToString(),
                        Current = nColumn.CurrentValue?.ToString(),
                        BatchID = lBatchID,
                        User = lUserID
                    });
                }
            }

            
            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Added))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);

                lNewRevisions.Add(new Revision
                {
                    ID = Guid.NewGuid(),
                    RecordID = lPrimaryKey,
                    Table = lTableName,
                    Column = "RECORD_CREATED",
                    BatchID = lBatchID,
                    User = lUserID
                });
            }
            this.Revisions.AddRange(lNewRevisions);
        }

        private static void GetRecordInfo(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry nRecord, out string aTableName, out string aPrimaryKey)
        {
            aTableName = nRecord.Metadata.GetTableName();
            var lPrimaryKeyNames = nRecord.Metadata.FindPrimaryKey().Properties.Select(aR => aR.Name);
            aPrimaryKey = null;
            if (lPrimaryKeyNames.Count() == 0)
            {
                aPrimaryKey = "NONE";
            }
            else if (lPrimaryKeyNames.Count() == 1)
            {
                aPrimaryKey = nRecord.Entity.GetType()
                    .GetProperty(lPrimaryKeyNames.First())
                    .GetValue(nRecord.Entity)
                    ?.ToString();
            }
            else
            {
                aPrimaryKey = String.Join(", ", lPrimaryKeyNames.Select(aR
                    => aR + ": " + nRecord.Entity.GetType()
                        .GetProperty(aR)
                        .GetValue(nRecord.Entity)
                        ?.ToString()));
            }
        }
    }
}
