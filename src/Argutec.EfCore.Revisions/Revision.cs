using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argutec.EfCore.Revisions
{
    public class Revision
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }
        [Required]
        public Guid BatchID { get; set; }
        [Required]
        public string RecordID { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [MaxLength(128)]
        public string Table { get; set; }
        [Required]
        [MaxLength(128)]
        public string Column { get; set; }
        public string Original { get; set; }
        public string Current { get; set; }
        [MaxLength(128)]
        public string User { get; set; }
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        public RevisionOperation BatchOperation { get; set; }
        [Required]
        [MaxLength(1024)]
        public string BatchTables { get; set; }
    }
}