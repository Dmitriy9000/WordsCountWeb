namespace Backend.Infrastructure.Model
{
    public interface IWordsDataContextFactory
    {
        WordsDataContext Create();
    }
}