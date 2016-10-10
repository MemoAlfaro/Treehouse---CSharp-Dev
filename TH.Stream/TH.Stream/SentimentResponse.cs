using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Stream
{
    public class SentimentResponse
    {
        [JsonProperty("documents")]
        public List<Sentiment> Sentiments { get; set; }
    }

    public class Sentiment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("score")]
        public string Score { get; set; }
    }
}
