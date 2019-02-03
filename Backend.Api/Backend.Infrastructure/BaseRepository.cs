using Backend.Infrastructure.Model;

namespace Backend.Infrastructure
{
    public class BaseRepository
    {
        protected readonly IWordsDataContextFactory ContextFactory;

        public BaseRepository(IWordsDataContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }
    }
}
