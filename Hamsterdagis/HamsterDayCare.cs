using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public class HamsterDayCare
    {
        //private static readonly List<Hamster> fileHamsters = new List<Hamster>();
      
        private static List<Hamster> arrivedHamsters = new List<Hamster>();


        public static void ReadFile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Hamsterlista30");
            string file = Directory.GetFiles(filePath, "*.csv").FirstOrDefault();

            if (file == null)
            {
                Console.WriteLine($"No files found in {filePath}");
            }
            else
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    using (StreamReader rdr = new StreamReader(fs))
                    {
                        while (!rdr.EndOfStream)
                        {
                            string line = rdr.ReadLine();
                            string[] splitted = line.Split(';');
                            arrivedHamsters.Add(new Hamster()
                            {
                                Name = splitted[0],
                                Age = int.Parse(splitted[1]),
                                Gender = char.Parse(splitted[2]),
                                OwnerName = splitted[3]
                            });
                        }
                    }
                }
            }
            

        }
        //public static List<Hamster> AddHamsters()
        //{
        //    //for (int i = 0; i < arrivedHamsters.Count; i++)
        //    //{
        //    //    arrivedHamsters.Add(new Hamster());
        //    //}
        //    foreach (var hamster in fileHamsters)
        //    {
        //        arrivedHamsters.Add(new Hamster());
        //    }
            
        //    return arrivedHamsters;
        //}
        public static void PrintHamsters()
        {
            foreach (var hamster in arrivedHamsters)
            {
                Console.WriteLine($"{hamster.Name} {hamster.Age} {hamster.Gender} {hamster.OwnerName}");
            }
           
        }
    }
}
