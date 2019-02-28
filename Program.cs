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
            List<string> fileNames = new List<string>() { "a_example.txt", "b_lovely_landscapes.txt",
                "c_memorable_moments.txt", "d_pet_pictures.txt", "e_shiny_selfies.txt" };
            foreach (var fileName in fileNames)
            {
                var modifiedFileName = fileName.Split(".".ToCharArray()).FirstOrDefault();

                int photoCount = 0;
                List<Photo> photoList = new List<Photo>();
                List<Photo> allPhotos = new List<Photo>();

                using (var streamReader = File.OpenText(Program.RootPath + modifiedFileName + ".txt"))
                {
                    var lines = streamReader.ReadToEnd().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    photoCount = Int32.Parse(lines[0]);

                    for (int i = 1; i < lines.Count(); i++)
                    {
                        string line = lines[i];
                        //Console.WriteLine(lines[i]);
                        //lineList.Add(line);
                        // Console.WriteLine(lines[i]);
                        //lineList.Add(line);

                        var a = line.Trim().Split(" ".ToCharArray());
                        string ort = a[0];

                        Photo photoInstance = new Photo();
                        photoInstance.Orientation = (ort.Equals("H") == true ? Photo.ePhotoOrientation.H : Photo.ePhotoOrientation.V);

                        for (int j = 2; j < a.Count(); j++)
                        {
                            string stringTag = a[j];
                            photoInstance.Tags.Add(stringTag);
                        }

                        allPhotos.Add(photoInstance);
                    }
                }

                //int finalStepCount = Int32.Parse(settingsString[5]);
                //for (int time = 0; time < finalStepCount; time++)
                //{
                //    // TODO: Iterate here
                //}


                // ============================ //
                //         WRITE RESULT         //
                // ============================ //

                if (!Directory.Exists(Program.OutPath))
                {
                    Directory.CreateDirectory(Program.OutPath);
                }

                using (TextWriter textWriter = new StreamWriter(Program.OutPath + modifiedFileName + "_out.txt"))
                {
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
