using System;
using Backend.Infrastructure;
using Backend.Infrastructure.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Backend.Domain.Tests
{
    [TestClass]
    public class StatsServiceTests
    {
        // System under test
        private StatsService _sut;

        // Mocks
        private Mock<ISessionRepository> _mockSessionRepository;

        [TestInitialize]
        public void Init()
        {
            _mockSessionRepository = new Mock<ISessionRepository>();
            _sut = new StatsService(_mockSessionRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void StatsService_GetSessionStats_NoSessionShouldThrowException()
        {
            _mockSessionRepository.Setup(c => c.GetSession(It.IsAny<Guid>())).Returns((Session)null);
            var stats = _sut.GetSessionStats(Guid.NewGuid());
        }

        [TestMethod]
        public void StatsService_GetSessionStats_ShouldReturnSessionStats()
        {
            var session = new Session()
            {
                RequestsPerformed = 123,
                TotalClientsCount = 321,
                WordsSubmitted = 888
            };
            _mockSessionRepository.Setup(c => c.GetSession(It.IsAny<Guid>())).Returns(session);

            var stats = _sut.GetSessionStats(Guid.NewGuid());

            stats.WordsSubmitted.Should().Be(session.WordsSubmitted);
            stats.TotalClientsCount.Should().Be(session.TotalClientsCount);
            stats.RequestsPerformed.Should().Be(session.RequestsPerformed);
            _mockSessionRepository.Verify(c => c.GetSession(It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void StatsService_WordsSubmitted_ShouldUpdateWords()
        {
            var id = Guid.NewGuid();
            var cnt = 15;

            _sut.WordsSubmitted(id, cnt, "testclient");

            _sut.Words[id].Should().Be(cnt);
        }

        [TestMethod]
        public void StatsService_RequestPerformed_ShouldUpdateRequests()
        {
            var id = Guid.NewGuid();

            _sut.RequestPerformed(id, "testclient1");
            _sut.RequestPerformed(id, "testclient2");

            _sut.Requests[id].Should().Be(2);
        }
    }
}
