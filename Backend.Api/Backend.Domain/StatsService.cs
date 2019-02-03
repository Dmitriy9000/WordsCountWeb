using Backend.Domain.Model;
using Backend.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Backend.Domain
{
    public class StatsService : IStatsService
    {
        public ConcurrentDictionary<Guid, long> Requests { get; private set; }
        public ConcurrentDictionary<Guid, long> Words { get; private set; }
        public ConcurrentDictionary<Guid, List<string>> Clients { get; private set; }

        private readonly ISessionRepository _sessionRepository;

        public StatsService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
            Requests = new ConcurrentDictionary<Guid, long>();
            Words = new ConcurrentDictionary<Guid, long>();
            Clients = new ConcurrentDictionary<Guid, List<string>>();
        }

        public SessionStats GetSessionStats(Guid sessionId)
        {
            var session = _sessionRepository.GetSession(sessionId);
            if (session != null)
                return new SessionStats()
                {
                    Id = session.Id,
                    RequestsPerformed = session.RequestsPerformed,
                    TotalClientsCount = session.TotalClientsCount,
                    WordsSubmitted = session.WordsSubmitted
                };

            throw new Exception("Session not found");
        }

        public void WordsSubmitted(Guid sessionId, long count, string clientId)
        {
            Words.AddOrUpdate(sessionId, count, (k, v) => { return v += count; });
            RequestPerformed(sessionId, clientId);
        }

        public void RequestPerformed(Guid sessionId, string clientId)
        {
            Requests.AddOrUpdate(sessionId, 1, (k, v) => v += 1);

            if (!string.IsNullOrWhiteSpace(clientId))
            {
                Clients.AddOrUpdate(
                sessionId,
                new List<string>() { clientId },
                (k, v) =>
                {
                    if (!v.Contains(clientId))
                        v.Add(clientId);
                    return v;
                });
            }

            UpdateSessionStats(new SessionStats()
            {
                Id = sessionId,
                RequestsPerformed = Requests[sessionId],
                TotalClientsCount = Clients[sessionId].Count,
                WordsSubmitted = Words.ContainsKey(sessionId) ? Words[sessionId] : 0
            });
        }

        private void UpdateSessionStats(SessionStats stats)
        {
            _sessionRepository.UpdateSessionStats(stats.Id, stats.WordsSubmitted, stats.RequestsPerformed, stats.TotalClientsCount);
        }
    }
}
