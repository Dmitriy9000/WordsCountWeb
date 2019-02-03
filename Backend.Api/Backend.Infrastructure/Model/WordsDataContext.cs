using System.Data.Entity;

namespace Backend.Infrastructure.Model
{
    public class WordsDataContext : DbContext
    {
        public WordsDataContext() : base("WordsDataContext") { }

        public WordsDataContext(string csName) : base(csName) { }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionWord> Words { get; set; }
    }
}
