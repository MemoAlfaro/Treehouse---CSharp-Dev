using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                string fileContents = ReadFile(file.FullName);

                string[] fileLines = fileContents.Split(new char[] { '\n' });
                foreach (var line in fileLines)
                {
                    Console.WriteLine(line);
                }

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

    }
}