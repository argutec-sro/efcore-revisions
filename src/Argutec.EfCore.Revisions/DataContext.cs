using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Argutec.EfCore.Revisions
{
    public class DataContext : DbContext
    {
        private readonly IAppContext mAppContext = null;

        public DataContext(IAppContext aAppContext = null) : base()
        {
            mAppContext = aAppContext;
        }
        public DataContext(DbContextOptions aOptions, IAppContext aAppContext = null) : base(aOptions)
        {
            mAppContext = aAppContext;
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

        protected override void OnModelCreating(ModelBuilder aModelBuilder)
        {
            base.OnModelCreating(aModelBuilder);

            aModelBuilder.Entity<Revision>().Property(aR => aR.BatchOperation).HasDefaultValue(RevisionOperation.Insert);
            aModelBuilder.Entity<Revision>().Property(aR => aR.BatchTables).HasDefaultValue("UNKNOWN");
        }

        public void SaveRevision()
        {
            Guid lBatchID = Guid.NewGuid();            
            
            string lUserID = mAppContext != null ? mAppContext.LoggedUser : null;
            string lUser = mAppContext != null ? mAppContext.LoggedUserName : null;

            List<Revision> lNewRevisions = new List<Revision>();

            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Modified))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);

                var lMembers = nRecord.Properties.Where(aR => aR.IsModified);
                foreach (var nColumn in lMembers)
                {
                    if (nColumn == null) continue;
                    
                    if ((nColumn.OriginalValue == null && nColumn.CurrentValue != null)
                     || (nColumn.OriginalValue != null && nColumn.CurrentValue == null)
                     || (nColumn.OriginalValue != null && !nColumn.OriginalValue.Equals(nColumn.CurrentValue)))
                    {
                        lNewRevisions.Add(new Revision
                        {
                            ID = Guid.NewGuid(),
                            RecordID = lPrimaryKey,
                            CreateDate = DateTime.UtcNow,
                            Table = lTableName,
                            Column = nColumn.Metadata.Name,
                            Original = nColumn.OriginalValue?.ToString(),
                            Current = nColumn.CurrentValue?.ToString(),
                            BatchID = lBatchID,
                            User = lUserID,
                            UserName = lUser
                        });
                    }
                }
            }

            
            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Added))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);

                lNewRevisions.Add(new Revision
                {
                    ID = Guid.NewGuid(),
                    RecordID = lPrimaryKey,
                    CreateDate = DateTime.UtcNow,
                    Table = lTableName,
                    Column = "RECORD_CREATED",
                    BatchID = lBatchID,
                    User = lUserID,
                    UserName = lUser
                });
            }

            foreach (var nBatch in lNewRevisions.GroupBy(aR => aR.BatchID))
            {
                var lTables = nBatch.Select(aR => aR.Table).Distinct().OrderBy(aR => aR);
                var lTablesJoined = String.Join(", ", lTables);
                var lBatchOperation = nBatch.All(aR => aR.Column == "RECORD_CREATED")
                    ? RevisionOperation.Insert
                    : RevisionOperation.Update;

                nBatch.ToList().ForEach(aR =>
                {
                    aR.BatchTables = lTablesJoined;
                    aR.BatchOperation = lBatchOperation;
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
