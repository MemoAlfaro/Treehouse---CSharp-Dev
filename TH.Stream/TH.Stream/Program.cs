using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;

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
                }

                fileName = Path.Combine(dirInfo.FullName, "topten.json");
                SerializePlayers(fileName, topTenPlayers);
           }
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
            //players.Reverse();

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
    }
}