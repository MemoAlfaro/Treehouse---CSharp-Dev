using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

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
                //List<string[]> FileContents = ReadSoccerResults(file.FullName);

                //string fileContents = ReadFile(file.FullName);

                //string[] fileLines = fileContents.Split(new char[] { '\n' });
                //foreach (var line in fileLines)
                //{
                //    Console.WriteLine(line);
                //}

                //Console.WriteLine(fileContents);

                //var reader = new StreamReader(file.FullName);
                //try
                //{
                //    Console.SetIn(reader);
                //    var fileContent = Console.ReadLine(); 
                //    Console.WriteLine(fileContent);
                //}
                //finally
                //{
                //    reader.Close();
                //}
                //using (var reader = new StreamReader(file.FullName))
                //{
                //    Console.SetIn(reader);
                //    Console.WriteLine(Console.ReadLine());
                //}

                //string myString= UnicodeEncoding.Unicode.GetString(new byte[] {});
                //Console.WriteLine(myString);
            }

            //var dirFiles = dirInfo.GetFiles("*.txt");
            //foreach (var file in dirFiles)
            //{
            //    Console.WriteLine(file.Name);
            //}
            //Console.ReadLine();
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        //public static List<string[]> ReadSoccerResults (string fileName)
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
    }
}