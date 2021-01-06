using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public void SaveRevision()
        {
            Guid lBatchID = Guid.NewGuid();            
            
            string lUserID = mAppContext != null ? mAppContext.LoggedUser : null;
            string lUser = mAppContext != null ? mAppContext.LoggedUserName : null;

            List<Revision> lNewRevisions = new List<Revision>();

            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Modified))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);
                string lAdditionalInformation = GetRecordAdditionalInformation(nRecord);

                var lMembers = nRecord.Properties.Where(aR => aR.IsModified);
                foreach (var nColumn in lMembers)
                {
                    lNewRevisions.Add(new Revision
                    {
                        ID = Guid.NewGuid(),
                        RecordID = lPrimaryKey,
                        AdditionalInfo = lAdditionalInformation,
                        CreateDate = DateTime.Now,
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

            
            foreach (var nRecord in this.ChangeTracker.Entries().Where(aR => aR.State == EntityState.Added))
            {
                GetRecordInfo(nRecord, out string lTableName, out string lPrimaryKey);
                string lAdditionalInformation = GetRecordAdditionalInformation(nRecord);

                lNewRevisions.Add(new Revision
                {
                    ID = Guid.NewGuid(),
                    RecordID = lPrimaryKey,
                    AdditionalInfo = lAdditionalInformation,
                    CreateDate = DateTime.Now,
                    Table = lTableName,
                    Column = "RECORD_CREATED",
                    BatchID = lBatchID,
                    User = lUserID,
                    UserName = lUser
                });
            }
            this.Revisions.AddRange(lNewRevisions);
        }

        private static string GetRecordAdditionalInformation(EntityEntry aEntity)
        {
            Dictionary<string, string> lValues = new Dictionary<string, string>();

            foreach (var nProperty in aEntity.Entity.GetType().GetProperties())
            {
                if (Attribute.IsDefined(nProperty, typeof(AdditionalInfoAttribute)))
                {
                    lValues.Add(nProperty.Name, nProperty.GetValue(aEntity.Entity)?.ToString());
                }
            }

            if (lValues.Count == 0) return null;

            return SerializeValues(lValues
                .OrderBy(aR => aR.Key)
                .Select(aR => aR.Value));
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
                aPrimaryKey = SerializeValues(lPrimaryKeyNames.Select(aR
                    => aR + ": " + nRecord.Entity.GetType()
                        .GetProperty(aR)
                        .GetValue(nRecord.Entity)
                        ?.ToString()));
            }
        }
        private static string SerializeValues(IEnumerable<string> aValues)
        {
            return String.Join(", ", aValues);
        }
    }
}
