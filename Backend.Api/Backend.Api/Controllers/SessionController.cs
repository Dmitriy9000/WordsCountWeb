using Backend.Api.Models;
using Backend.Domain;
using System;
using System.Web.Http.Description;

namespace Backend.Api.Controllers
{
    public class SessionController : BaseController
    {
        private readonly IWordsService _wordsService;

        public SessionController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        // POST: api/Session
        [ResponseType(typeof(ServerResponse<Guid>))]
        public ServerResponse Post()
        {
            try
            {
                var sessionId = _wordsService.OpenSession();
                return ServerResponse<Guid>.Success(sessionId);
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }

        // DELETE: api/Session/5
        // [HttpDelete, Route("{sessionId}")]
        [ResponseType(typeof(ServerResponse))]
        public ServerResponse Delete(Guid id)
        {
            try
            {
                _wordsService.CloseSession(id);
                return ServerResponse.Success();
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }
    }
}
