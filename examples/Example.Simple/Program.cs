using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Example.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DataContext lDataContext = new DataContext(DesignTimeDbContextFactory.CONNECTION_STRING, new AppContext()))
            {
                lDataContext.Database.Migrate();
                
                lDataContext.Books.Add(new Book()
                {
                    ID = Guid.NewGuid(),
                    Name = "Lord of the Rings",
                    Year = 2000
                });

                lDataContext.Books.Add(new Book()
                {
                    ID = Guid.NewGuid(),
                    Name = "Lord of the Flies",
                    Year = 2000
                });

                lDataContext.Books.Add(new Book()
                {
                    ID = Guid.NewGuid(),
                    Name = "Lord of the Hobbits",
                    Year = 2000
                });

                var lTestBook = new Book()
                {
                    ID = Guid.NewGuid(),
                    Name = "TEST BOOK",
                    Year = 2000
                };
                lDataContext.Books.Add(lTestBook);

                lDataContext.SaveChanges();

                var lBook = lDataContext.Books.First();
                lBook.ReleaseDate = (new DateTime(2111, 1, 1)).ToUniversalTime();

                lDataContext.SaveChanges();
            }
        }
    }
}
