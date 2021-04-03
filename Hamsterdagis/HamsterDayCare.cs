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

        private static List<Hamster> hamsterList;
        private DateTime startDate;
        private ExerciseArea exerciseArea;
        private Cage cage;


        public HamsterDayCare()
        {
            ReadFile();
            PrintMaleHamsters();
            Console.WriteLine("");
            PrintFemaleHamsters();
        }
        

        public static void ReadFile()
        {
            var dbContext = new HamsterDBContext();
            if (!dbContext.Hamsters.Any())
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Hamsterlista30");
                string file = Directory.GetFiles(filePath, "*.csv").FirstOrDefault();

                if (file == null)
                {
                    Console.WriteLine($"No files found in {filePath}");
                }
                else
                {
                    using FileStream fs = new(file, FileMode.Open);
                    using StreamReader rdr = new(fs);
                    while (!rdr.EndOfStream)
                    {
                        string line = rdr.ReadLine();
                        string[] splitted = line.Split(';');
                        dbContext.Add(new Hamster()
                        {
                            Name = splitted[0],
                            Age = int.Parse(splitted[1]),
                            Gender = char.Parse(splitted[2]),
                            OwnerName = splitted[3]
                        });
                        dbContext.SaveChanges();
                    }
                }
                
               

            }
            

        }
        
        #region Testmethods (Print hamsters)
        public static void PrintAllHamsters()
        {
            var dbContext = new HamsterDBContext();
            var query = from h in dbContext.Hamsters
                        select h;
                       
                        
            foreach (var hamster in query)
            {
                Console.WriteLine($"Name: {hamster.Name} Age: {hamster.Age} Gender: {hamster.Gender} Owner: {hamster.OwnerName}");
            }
           
        }
        public static void PrintMaleHamsters()
        {
            var dbContext = new HamsterDBContext();
            var query = from h in dbContext.Hamsters
                        where h.Gender == 'M'
                        select h;


            foreach (var hamster in query)
            {
                Console.WriteLine($"Name: {hamster.Name} Age: {hamster.Age} Gender: {hamster.Gender} Owner: {hamster.OwnerName}");
            }

        }
        public static void PrintFemaleHamsters()
        {
            var dbContext = new HamsterDBContext();
            var query = from h in dbContext.Hamsters
                        where h.Gender == 'K'
                        select h;


            foreach (var hamster in query)
            {
                Console.WriteLine($"Name: {hamster.Name} Age: {hamster.Age} Gender: {hamster.Gender} Owner: {hamster.OwnerName}");
            }

        }
        #endregion
    }
}
