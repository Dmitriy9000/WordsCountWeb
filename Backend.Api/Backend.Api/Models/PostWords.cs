using System;

namespace Backend.Api.Models
{
    public class PostWords
    {
        public Guid SessionId { get; set; }
        public string[] Words { get; set; }
    }
}