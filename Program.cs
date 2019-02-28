using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.Classes;

namespace googlehashcode2019 {
    class Program
    {
        private static string RootPath = "Resources/";
        private static string OutPath = Program.RootPath + "Output/";

        static void Main(string[] args)
        {
            List<string> fileNames = new List<string>() {
                "a_example.txt", 
                "b_lovely_landscapes.txt",
                "c_memorable_moments.txt", 
                "d_pet_pictures.txt",  
                "e_shiny_selfies.txt"
                };

            foreach (var fileName in fileNames)
            {
                var modifiedFileName = fileName.Split(".".ToCharArray()).FirstOrDefault();

                int photoCount = 0;
                List<Photo> photoList = new List<Photo>();

                using (var streamReader = File.OpenText(Program.RootPath + modifiedFileName + ".txt"))
                {
                    var lines = streamReader.ReadToEnd().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    photoCount = Int32.Parse(lines[0]);

                    for (int i = 1; i < lines.Count(); i++)
                    {
                        string line = lines[i];

                        Photo photo = new Photo(line, i - 1);
                        photoList.Add(photo);
                    }
                }

                // ============================ //
                //         PROCESSING           //
                // ============================ //


                List<Slide> slideList = SimpleMatching.GetSlides(photoList);


                // ============================ //
                //         WRITE RESULT         //
                // ============================ //

                if (!Directory.Exists(Program.OutPath))
                {
                    Directory.CreateDirectory(Program.OutPath);
                }

                using (TextWriter textWriter = new StreamWriter(Program.OutPath + modifiedFileName + "_out.txt"))
                {
                    textWriter.WriteLine(slideList.Count);
                    foreach (Slide s in slideList)
                    {
                        textWriter.WriteLine(s.ToString());
                    }
                    Console.WriteLine("File '" + modifiedFileName + "' processed");
                }
            }

            Console.WriteLine("Completed!");
            Console.ReadKey();
        }
    }
}
