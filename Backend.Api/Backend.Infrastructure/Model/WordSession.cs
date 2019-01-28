using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Infrastructure.Model
{
    public class WordSession
    {
        [Key, Column(Order = 0)]
        public Guid SessionId { get; set; }

        [Key, Column(Order = 1)]
        public string Word { get; set; }

        public ulong Count { get; set; }
    }
}
