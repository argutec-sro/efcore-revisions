using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Example.Simple
{
    public class Book
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
