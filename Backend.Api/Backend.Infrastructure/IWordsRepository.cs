using System;
using System.Collections.Generic;

namespace Backend.Infrastructure
{
    public interface IWordsRepository
    {
        void AddWord(Guid sessionId, string word, int count);
        long CountWord(Guid sessionId, string word);
        Dictionary<string, long> CountWords(Guid sessionId);
    }
}