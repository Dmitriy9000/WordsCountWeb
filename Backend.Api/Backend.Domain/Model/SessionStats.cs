using System;

namespace Backend.Domain.Model
{
    public class SessionStats
    {
        public Guid Id { get; set; }

        public long WordsSubmitted { get; set; }
        public long RequestsPerformed { get; set; }
        public long TotalClientsCount { get; set; }
    }
}
