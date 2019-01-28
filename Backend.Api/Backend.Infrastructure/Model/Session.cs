using System;

namespace Backend.Infrastructure.Model
{
    public class Session
    {
        public Guid Id { get; set; }
        public bool IsOpen { get; set; }
    }
}
