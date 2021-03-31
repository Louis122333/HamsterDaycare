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
        private static List<Hamster> hamsterMaleList = new List<Hamster>();
        private static List<Hamster> hamsterFemaleList = new List<Hamster>();
        
        
        
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
                            if (splitted[2] == "M")
                            {
                                hamsterMaleList.Add(new Hamster()
                                {
                                    Name = splitted[0],
                                    Age = int.Parse(splitted[1]),
                                    Gender = char.Parse(splitted[2]),
                                    OwnerName = splitted[3]

                                });
                            }
                            else if (splitted[2] == "K")
                            {
                                hamsterFemaleList.Add(new Hamster()
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
            

        }
        public static void PrintHamsters()
        {
            Console.WriteLine("Males: ");
            foreach (var male in hamsterMaleList)
            {
                Console.WriteLine($"Name: {male.Name} Age: {male.Age} Gender: {male.Gender} Owner: {male.OwnerName}");
            }
            Console.WriteLine("\nFemales: ");
            foreach (var female in hamsterFemaleList)
            {
                Console.WriteLine($"Name: {female.Name} Age: {female.Age} Gender: {female.Gender} Owner: {female.OwnerName}");
            }
           
        }
    }
}
