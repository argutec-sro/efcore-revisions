using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Simple
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public const string CONNECTION_STRING = "Host=127.0.0.1;Port=9004;Username=revisions;Password=heslo;Database=revisions;";

        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext(CONNECTION_STRING);
        }
    }
}
