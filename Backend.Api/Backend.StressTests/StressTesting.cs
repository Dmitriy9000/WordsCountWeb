using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Backend.StressTests
{
    [TestClass]
    public class StressTesting
    {
        private RestClient _restClient;

        [TestInitialize]
        public void Init()
        {
            _restClient = new RestClient("http://13.94.225.154");
        }

        [TestMethod]
        public void PushMessages()
        {
            var request = new RestRequest("api/session", Method.POST);
            
            var sessionResp = _restClient.Execute(request);
            var sessionObj = JsonConvert.DeserializeObject(sessionResp.Content) as JObject;
            var sessionId = sessionObj.Value<string>("Data");

            var requestsCount = 100;
            var range = Enumerable.Range(0, requestsCount);
            Parallel.ForEach(range, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, (i) => PushBatch(sessionId));

            request = new RestRequest($"api/words/{sessionId}/he", Method.GET);
            var countResp = JsonConvert.DeserializeObject(_restClient.Execute(request).Content) as JObject;

            var count = countResp.Value<int>("Data");
            Assert.AreEqual(requestsCount * 2, count);
        }

        private void PushBatch(string sessionId)
        {
            var request = new RestRequest("api/words", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                SessionId = sessionId,
                Words = WordsToPush()
            });
            var wordsResp = _restClient.Execute(request);
        }

        private string[] WordsToPush()
        {
            return new[]
            {
                "Kafka",
                "feared",
                "that",
                "people",
                "would",
                "find",
                "him",
                "mentally",
                "and",
                "physically",
                "repulsive",
                "However",
                "those",
                "who",
                "met",
                "him",
                "found",
                "him",
                "to",
                "possess",
                "a",
                "quiet",
                "and",
                "cool",
                "demeanor",
                "obvious",
                "intelligence",
                "and",
                "a",
                "dry",
                "sense",
                "of",
                "humour",
                "they",
                "also",
                "found",
                "him",
                "boyishly",
                "handsome",
                "although",
                "of",
                "austere",
                "appearance",
                "Brod",
                "compared",
                "Kafka",
                "to",
                "Heinrich",
                "von",
                "Kleist",
                "noting",
                "that",
                "both",
                "writers",
                "had",
                "the",
                "ability",
                "to",
                "describe",
                "a",
                "situation",
                "realistically",
                "with",
                "precise",
                "details",
                "Brod",
                "thought",
                "Kafka",
                "was",
                "one",
                "of",
                "the",
                "most",
                "entertaining",
                "people",
                "he",
                "had",
                "met",
                "Kafka",
                "enjoyed",
                "sharing",
                "humour",
                "with",
                "his",
                "friends",
                "but",
                "also",
                "helped",
                "them",
                "in",
                "difficult",
                "situations",
                "with",
                "good",
                "advice",
                "According",
                "to",
                "Brod",
                "he",
                "was",
                "a",
                "passionate",
                "reciter",
                "who",
                "was",
                "able",
                "to",
                "phrase",
                "his",
                "speaking",
                "as",
                "if",
                "it",
                "were",
                "music",
                "Brod",
                "felt",
                "that",
                "two",
                "of",
                "Kafka's",
                "most",
                "distinguishing",
                "traits",
                "were",
                "absolute",
                "truthfulness",
                "(absolute Wahrhaftigkeit)",
                "and",
                "precise conscientiousness",
                "präzise",
                "Gewissenhaftigkeit",
                "He",
                "explored",
                "details",
                "the",
                "inconspicuous",
                "in",
                "depth",
                "and",
                "with",
                "such",
                "love",
                "and",
                "precision",
                "that",
                "things",
                "surfaced",
                "that",
                "were",
                "unforeseen",
                "seemingly",
                "strange",
                "but",
                "absolutely",
                "true",
                "(nichts als wahr)",
            };
        }
    }
}
