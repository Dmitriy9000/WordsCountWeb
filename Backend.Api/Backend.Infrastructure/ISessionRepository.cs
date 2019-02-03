using Backend.Infrastructure.Model;
using System;

namespace Backend.Infrastructure
{
    public interface ISessionRepository
    {
        void CloseSession(Guid sessionId);
        Guid OpenSession();
        void UpdateSessionStats(Guid sessionId, long wordsSubmitted, long requestsPerformed, long totalClientsCount);
        Session GetSession(Guid sessionId);
    }
}