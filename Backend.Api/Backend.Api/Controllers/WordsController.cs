using Backend.Api.Models;
using Backend.Domain;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Api.Controllers
{
    public class WordsController : BaseController
    {
        private readonly IWordsService _wordsService;
        private readonly IStatsService _statsService;

        public WordsController(IWordsService wordsService, IStatsService statsService)
        {
            _wordsService = wordsService;
            _statsService = statsService;
        }

        // GET: api/Words/{sessionId}/{word}
        [Route("api/Words/{sessionId}/{word}")]
        [ResponseType(typeof(ServerResponse<long>))]
        public ServerResponse Get(Guid sessionId, string word)
        {
            try
            {
                var count = _wordsService.CountWord(sessionId, word);
                _statsService.RequestPerformed(sessionId, GetClientIp());
                return ServerResponse<long>.Success(count);
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }

        // GET: api/Words/{sessionId}
        [Route("api/Words/{sessionId}")]
        [ResponseType(typeof(ServerResponse<WordEntry[]>))]
        public ServerResponse Get(Guid sessionId)
        {
            try
            {
                var words = _wordsService.CountWords(sessionId)
                    .Select(c => new WordEntry() { Word = c.Key, Count = c.Value })
                    .ToArray();
                _statsService.RequestPerformed(sessionId, GetClientIp());
                return ServerResponse<WordEntry[]>.Success(words);
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }

        // POST: api/Words
        [ResponseType(typeof(ServerResponse))]
        public ServerResponse Post([FromBody]PostWords postedWords)
        {
            try
            {
                _wordsService.AddWord(postedWords.SessionId, postedWords.Words);
                _statsService.WordsSubmitted(postedWords.SessionId, postedWords.Words.Length, GetClientIp());
                return ServerResponse.Success();
            }
            catch (Exception exception)
            {
                return ServerResponse.Fail(exception.Message);
            }
        }
    }
}
