using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Simple
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public const string CONNECTION_STRING = "Host=localhost;Port=9001;Username=revisions;Password=develpass;Database=revisions;";

        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext(CONNECTION_STRING);
        }
    }
}
