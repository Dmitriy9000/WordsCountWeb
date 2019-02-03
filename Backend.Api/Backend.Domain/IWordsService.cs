using System;
using System.Collections.Generic;

namespace Backend.Domain
{
    public interface IWordsService
    {
        void AddWord(Guid sessionId, string[] words);
        void CloseSession(Guid sessionId);
        Guid OpenSession();
        long CountWord(Guid sessionId, string word);
        Dictionary<string, long> CountWords(Guid sessionId);
    }
}