using System.Data.Entity;

namespace Backend.Infrastructure.Model
{
    public class WordsDataContext : DbContext
    {
        public WordsDataContext() : base("WordDataContext") { }

        public WordsDataContext(string csName) : base(csName) { }

        public DbSet<Session> Sessions { get; set; }
    }
}
