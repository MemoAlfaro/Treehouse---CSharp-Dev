using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;

namespace TH.Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo dirInfo = new DirectoryInfo(currentDirectory);

            var fileName = Path.Combine(dirInfo.FullName, "SoccerGameResults.csv");
            var file = new FileInfo(fileName);

            if (file.Exists)
            {
                List<GameResult> soccerResults = ReadSoccerResults(file.FullName);
            }

            fileName = Path.Combine(dirInfo.FullName, "players.json");
            file= new FileInfo (fileName);
            if (file.Exists)
            {
                List<Player> players = DeserializePlayers(file.FullName);
                List<Player> topTenPlayers = GetTopTenPlayers(players);

                foreach (var player in topTenPlayers)
                {
                    Console.WriteLine(player.FirstName 
                        + " | " + player.SecondName 
                        + " | Points: " + player.PointsPerGame);

                    List<NewsResult> newsResult= GetNewsForPlayer
                        (string.Format("{0} {1}", player.FirstName, player.SecondName));

                    var sentimentResponse= GetSentimentResponse(newsResult);
                    foreach (var sentiment in sentimentResponse.Sentiments)
                    {
                        foreach (var news in newsResult)
                        {
                            if (news.Headline == sentiment.Id)
                            {
                                double score;
                                if (double.TryParse(sentiment.Score, out score))
                                    news.SentimentScore= score;

                                break;
                            }
                        }
                    }

                    foreach (var result in newsResult)
                    {
                        Console.WriteLine(string.Format(
                            "Sentiment Score: {0:p} Date: {1:f}, Headline: {2}, Summary: {3} \n",
                            result.SentimentScore, result.DatePublished, result.Headline, result.Summary));

                        //Console.ReadKey();
                    }
                }

                fileName = Path.Combine(dirInfo.FullName, "topten.json");
                SerializePlayers(fileName, topTenPlayers);
            }

            //string googleString= GetGoogleHomePage();
            //Console.WriteLine(GetNewsForPlayer("Messi"));
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<GameResult> ReadSoccerResults (string fileName)
        {
            //List<string[]> fileContents = new List<string[]>();
            List<GameResult> fileContents= new List<GameResult>();
            using (var reader = new StreamReader(fileName))
            {
                CultureInfo culture;
                DateTimeStyles styles;

                culture = CultureInfo.CreateSpecificCulture("en-US");
                styles = DateTimeStyles.None;

                string line = "";
                reader.ReadLine(); // read header
                while ((line = reader.ReadLine()) != null)
                {
                    var gameResult = new GameResult();
                    string[] values = line.Split(',');
                    //fileContents.Add(line.Split(','));

                    DateTime gameDate;
                    if (DateTime.TryParse(values[0], culture, styles, out gameDate))
                        gameResult.GameDate = gameDate;

                    gameResult.TeamName = values[1];

                    HomeOrAway homeOrAway;
                    if (Enum.TryParse(values[2], out homeOrAway))
                        gameResult.HomeOrAway = homeOrAway;

                    int parseInt;
                    if (int.TryParse(values[3], out parseInt))
                        gameResult.Goals = parseInt;

                    if (int.TryParse(values[4], out parseInt))
                        gameResult.GoalAttempts = parseInt;

                    if (int.TryParse(values[5], out parseInt))
                        gameResult.ShotsOnGoal = parseInt;

                    if (int.TryParse(values[6], out parseInt))
                        gameResult.ShotsOffGoal = parseInt;

                    double parseDouble;
                    if (double.TryParse(values[7], out parseDouble))
                        gameResult.PossesionPercent = parseDouble;

                    fileContents.Add(gameResult);
                }
            }
            return fileContents;
        }

        public static List<Player> DeserializePlayers (string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(fileName))
            using (var jsonReader= new JsonTextReader (reader))
            {
                players= serializer.Deserialize<List<Player>>(jsonReader);
            }

            return players;
        }

        public static List<Player> GetTopTenPlayers (List<Player> players)
        {
            List<Player> topTenPlayers = new List<Player>();
            players.Sort();

            int ii = 1;
            foreach (var player in players)
            {
                topTenPlayers.Add(player);

                if (ii++ == 10)
                    break;
            }

            return topTenPlayers;
        }

        public static void SerializePlayers(string fileName, List<Player> players)
        {
            var serializer = new JsonSerializer();

            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, players);
            }
        }

        public static string GetGoogleHomePage()
        {
            WebClient webClient = new WebClient();
            byte[] googleBytes = webClient.DownloadData("https://www.google.com");

            using (var stream= new MemoryStream(googleBytes))
            using (var reader= new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<NewsResult> GetNewsForPlayer(string playerName)
        {
            List<NewsResult> newsResult = new List<NewsResult>();

            string searchString = string.Format
                ("https://api.cognitive.microsoft.com/bing/v5.0/news/search?q={0} HTTP/1.1", playerName);

            WebClient webClient = new WebClient();
            // key from Bing
            webClient.Headers.Add("Ocp-Apim-Subscription-Key", "xxx-yyy-zzz");

            byte[] searchResults= 
                webClient.DownloadData(searchString);

            JsonSerializer serializer = new JsonSerializer();

            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader (reader))
            {
                //return reader.ReadToEnd();
                newsResult = serializer.Deserialize<NewsSearch>(jsonReader).NewsResult;
            }

            return newsResult;
        }

        public static SentimentResponse GetSentimentResponse (List<NewsResult> newsResult)
        {
            var sentimentResponse = new SentimentResponse();
            if (newsResult.Count() <= 0)
            {
                sentimentResponse.Sentiments = new List<Sentiment>();
                return sentimentResponse;
            }
            var sentimentRequest = new SentimenRequest();
            sentimentRequest.Documents = new List<Document>();

            foreach (var result in newsResult)
            {
                sentimentRequest.Documents.Add(new Document
                {
                    Language= "en",
                    Id= result.Headline,
                    Text= result.Summary
                });
            }

            WebClient webClient = new WebClient();

            string searchString = string.Format
                ("https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment");

            // key from Azure Cognitive Services
            webClient.Headers.Add("Ocp-Apim-Subscription-Key", "xxx-yyy-zzz");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");

            string requestJson = JsonConvert.SerializeObject(sentimentRequest);
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            byte[] response =
                webClient.UploadData(searchString, requestBytes);

            string sentiments= Encoding.UTF8.GetString(response);
            sentimentResponse= JsonConvert.DeserializeObject<SentimentResponse>(sentiments);

            return sentimentResponse;
        }
    }
}