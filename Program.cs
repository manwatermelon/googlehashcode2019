using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace googlehashcode2019 {
    class Program
    {
        private static string RootPath = "Resources/";
        private static string OutPath = Program.RootPath + "Output/";

        static void Main(string[] args)
        {
            List<string> fileNames = new List<string>() { "a_example.in", "b_should_be_easy.in",
                "c_no_hurry.in", "d_metropolis.in", "e_high_bonus.in" };
            foreach (var fileName in fileNames)
            {
                string[] settingsString;
                var modifiedFileName = fileName.Split(".".ToCharArray()).FirstOrDefault();

                List<string> lineList = new List<string>();
                using (var streamReader = File.OpenText(Program.RootPath + modifiedFileName + ".in"))
                {
                    var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    settingsString = lines[0].Split(" ".ToCharArray());

                    for (int i = 1; i < lines.Count(); i++)
                    {
                        string line = lines[i];
                        Console.WriteLine(lines[i]);
                        lineList.Add(line);
                    }
                }

                int finalStepCount = Int32.Parse(settingsString[5]);
                for (int time = 0; time < finalStepCount; time++)
                {
                    // TODO: Iterate here
                }


                // ============================ //
                //         WRITE RESULT         //
                // ============================ //

                if (!Directory.Exists(Program.OutPath))
                {
                    Directory.CreateDirectory(Program.OutPath);
                }

                using (TextWriter textWriter = new StreamWriter(Program.OutPath + modifiedFileName + ".out"))
                {
                    textWriter.WriteLine(String.Join(" ", settingsString));
                    foreach (string line in lineList)
                    {
                        textWriter.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("Completed!");
            Console.ReadKey();
        }
    }
}
