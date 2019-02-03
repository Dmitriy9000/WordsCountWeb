using Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Domain
{
    public class WordsService : IWordsService
    {
        private readonly IWordsRepository _wordRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ICacheService _cacheService;

        private const int CachingTimeSeconds = 2;

        public WordsService(
            IWordsRepository wordsRepository, 
            ISessionRepository sessionRepository,
            ICacheService cacheService)
        {
            _wordRepository = wordsRepository;
            _sessionRepository = sessionRepository;
            _cacheService = cacheService;
        }

        public Guid OpenSession()
        {
            return _sessionRepository.OpenSession();
        }

        public void CloseSession(Guid sessionId)
        {
            _sessionRepository.CloseSession(sessionId);
        }

        public void AddWord(Guid sessionId, string[] words)
        {
            var groupedWords = words.GroupBy(c => c);
            foreach (var word in groupedWords)
            {
                _wordRepository.AddWord(sessionId, word.Key, 1);
            }
        }

        public long CountWord(Guid sessionId, string word)
        {
            var words = CountWords(sessionId);
            word = word.ToLower();
            if (words.ContainsKey(word))
                return words[word];
            return 0;
        }

        public Dictionary<string, long> CountWords(Guid sessionId)
        {
            return _cacheService.Get(sessionId.ToString(), CachingTimeSeconds, 
                () => _wordRepository.CountWords(sessionId));
        }
    } 
}
