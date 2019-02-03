using System;
using Backend.Domain.Model;

namespace Backend.Domain
{
    public interface IStatsService
    {
        SessionStats GetSessionStats(Guid sessionId);
        void RequestPerformed(Guid sessionId, string clientId);
        void WordsSubmitted(Guid sessionId, long count, string clientId);
    }
}