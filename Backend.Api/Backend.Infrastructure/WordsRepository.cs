using Backend.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure
{
    public class WordsRepository : BaseRepository, IWordsRepository
    {
        public WordsRepository(IWordsDataContextFactory contextFactory) : base(contextFactory) { }

        public void AddWord(Guid sessionId, string word, int count)
        {
            using (var dc = ContextFactory.Create())
            {
                var openSession = dc.Sessions.SingleOrDefault(c => c.Id == sessionId && c.IsActive);
                if (openSession == null)
                    throw new Exception("Session not found");

                word = word.ToLower();

                var wordEntity = dc.Words.Find(sessionId, word);
                if (wordEntity == null)
                {
                    wordEntity = new SessionWord()
                    {
                        SessionId = sessionId,
                        Count = Convert.ToInt64(count),
                        Word = word
                    };
                    dc.Words.Add(wordEntity);
                }
                else
                {
                    wordEntity.Count += Convert.ToInt64(count);
                }
                
                dc.SaveChanges();
            }
        }

        public long CountWord(Guid sessionId, string word)
        {
            using (var dc = ContextFactory.Create())
            {
                var openSession = dc.Sessions.SingleOrDefault(c => c.Id == sessionId && c.IsActive);
                if (openSession == null)
                    throw new Exception("No active session");

                word = word.ToLower();

                var wordEntity = dc.Words.Find(sessionId, word);
                if (wordEntity == null)
                {
                    return 0;
                }
                else
                {
                    return wordEntity.Count;
                }
            }
        }

        public Dictionary<string, long> CountWords(Guid sessionId)
        {
            using (var dc = ContextFactory.Create())
            {
                var openSession = dc.Sessions.SingleOrDefault(c => c.Id == sessionId && c.IsActive);
                if (openSession == null)
                    throw new Exception("No active session");

                var words = dc.Words.Where(c => c.SessionId == sessionId);
                return words.ToDictionary(k => k.Word, v => v.Count);
            }
        }
    }
}
