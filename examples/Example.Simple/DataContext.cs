using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Example.Simple
{
    public class DataContext : Argutec.EfCore.Revisions.DataContext
    {
        protected string mConnectionString;
        public DataContext(DbContextOptions<DataContext> aOptions) : base(aOptions)
        {

        }
        public DataContext(string aConnectionString) : base()
        {
            mConnectionString = aConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder aOptionsBuilder)
        {
            base.OnConfiguring(aOptionsBuilder);
            if (mConnectionString != null)
                aOptionsBuilder.UseNpgsql(mConnectionString);
        }

        #region DbSets
        public virtual DbSet<Book> Books { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder aModelBuilder)
        {
        }
    }
}
