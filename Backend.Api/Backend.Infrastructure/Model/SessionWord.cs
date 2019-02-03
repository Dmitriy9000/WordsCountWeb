using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Infrastructure.Model
{
    [Table("Words")]
    public class SessionWord
    {
        [Key, Column(Order = 0)]
        public Guid SessionId { get; set; }

        [Key, Column(Order = 1)]
        public string Word { get; set; }

        public long Count { get; set; }
    }
}
