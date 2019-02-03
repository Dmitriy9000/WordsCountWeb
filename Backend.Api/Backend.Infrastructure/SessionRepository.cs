using Backend.Infrastructure.Model;
using System;

namespace Backend.Infrastructure
{
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository(IWordsDataContextFactory contextFactory) : base(contextFactory) { }

        public Guid OpenSession()
        {
            using (var dc = ContextFactory.Create())
            {
                var newSession = new Session()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    RequestsPerformed = 0,
                    TotalClientsCount = 0,
                    WordsSubmitted = 0
                };
                dc.Sessions.Add(newSession);
                dc.SaveChanges();
                return newSession.Id;
            }
        }

        public void CloseSession(Guid sessionId)
        {
            using (var dc = ContextFactory.Create())
            {
                var session = dc.Sessions.Find(sessionId);
                if (session == null)
                    return;

                session.IsActive = false;
                dc.SaveChanges();
            }
        }

        public void UpdateSessionStats(
            Guid sessionId,
            long wordsSubmitted, 
            long requestsPerformed, 
            long totalClientsCount)
        {
            using (var dc = ContextFactory.Create())
            {
                var session = dc.Sessions.Find(sessionId);
                if (session == null)
                    return;

                session.WordsSubmitted = wordsSubmitted;
                session.RequestsPerformed = requestsPerformed;
                session.TotalClientsCount = totalClientsCount;
                dc.SaveChanges();
            }
        }

        public Session GetSession(Guid sessionId)
        {
            using (var dc = ContextFactory.Create())
            {
                var session = dc.Sessions.Find(sessionId);
                return session;
            }
        }
    }
}
