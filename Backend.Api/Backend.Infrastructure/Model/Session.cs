using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Infrastructure.Model
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public long WordsSubmitted { get; set; }
        public long RequestsPerformed { get; set; }
        public long TotalClientsCount { get; set; }
    }
}
