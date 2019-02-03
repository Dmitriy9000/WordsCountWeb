using Backend.Api.Models;
using Backend.Domain;
using Backend.Domain.Model;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Api.Controllers
{
    public class MetricsController : ApiController
    {
        private readonly IStatsService _statsService;

        public MetricsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        // GET: api/Metrics/5
        [ResponseType(typeof(ServerResponse<SessionStats>))]
        public ServerResponse Get(Guid id)
        {
            try
            {
                var stats = _statsService.GetSessionStats(id);
                return ServerResponse<SessionStats>.Success(stats);
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }
    }
}
