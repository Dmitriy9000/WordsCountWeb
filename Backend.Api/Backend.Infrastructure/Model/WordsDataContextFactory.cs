namespace Backend.Infrastructure.Model
{
    public class WordsDataContextFactory : IWordsDataContextFactory
    {
        public WordsDataContext Create()
        {
            return new WordsDataContext();
        }
    }
}
