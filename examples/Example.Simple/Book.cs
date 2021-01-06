using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Argutec.EfCore.Revisions;

namespace Example.Simple
{
    public class Book
    {
        [Key]
        public Guid ID { get; set; }
        
        [AdditionalInfo]
        public string Name { get; set; }
        [AdditionalInfo]
        public int Year { get; set; }
    }
}
