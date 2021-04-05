using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public class HamsterDayCare
    {
        

        public HamsterDayCare()
        {

            //LoadHamsters();
            //CreatCages();
            //CreateExerciseArea();
            //PlaceHamstersInCages();
            MoveHamstersToExercise();

            //PrintMaleHamsters();
            //Console.WriteLine("");
            //PrintFemaleHamsters();
        }
        
        public static void CreateExerciseArea()
        {
            var dbContext = new HamsterDBContext();
            if (!dbContext.ExerciseArea.Any())
            {
                dbContext.ExerciseArea.Add(new ExerciseArea());
                dbContext.SaveChanges();
            }
        }
            
        public static void CreateCages()
        {
            var dbcontext = new HamsterDBContext();
            if (!dbcontext.Cages.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    dbcontext.Cages.Add(new Cage());
                }
            }
            dbcontext.SaveChanges();
        }
        public static bool LoadHamsters()
        {
            bool needtoLoad = false;
            var dbContext = new HamsterDBContext();
            if (!dbContext.Hamsters.Any())
            {
                needtoLoad = true;
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

            return needtoLoad;
        }

        public static bool PlaceHamstersInCages()
        {
            var dbContext = new HamsterDBContext();

            var femaleQueue = new Queue<Hamster>();
            var maleQueue = new Queue<Hamster>();

            foreach (var hamster in dbContext.Hamsters)
            {
                if (hamster.Gender == 'K')
                {
                    femaleQueue.Enqueue(hamster);
                }
                else if (hamster.Gender == 'M')
                {
                    maleQueue.Enqueue(hamster);
                }
            }
            foreach (var cage in dbContext.Cages)
            {
                if (femaleQueue.Count > 0)
                {
                    for (int i = 0; i < cage.Size; i++)
                    {
                        var hamster = femaleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        hamster.ActivityLogs.Add(new ActivityLog(DateTime.Now, Activity.Caged));
                    }
                }
                else
                {
                    for (int i = 0; i < cage.Size; i++)
                    {
                        var hamster = maleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        hamster.ActivityLogs.Add(new ActivityLog(DateTime.Now, Activity.Caged));
                    }
                }
            }
            dbContext.SaveChanges();
            return true;
        }
       
        public static void MoveHamstersToExercise()
        {
            var dbContext = new HamsterDBContext();
            var exerciseArea = dbContext.ExerciseArea.First();
            Hamster h = new Hamster();
            char gender = h.Gender;
            int counter = 0;
            foreach (var cage in dbContext.Cages)
            {
                foreach (var hamster in cage.Hamsters)
                {
                    if (counter == 0)
                    {
                        gender = hamster.Gender;
                    }
                    if (hamster.Gender == gender && counter < exerciseArea.Size)
                    {
                        exerciseArea.Hamsters.Add(hamster);
                        hamster.LastTimeExercised = DateTime.Now;
                        counter++;
                    }
                }
            }
            dbContext.SaveChanges();
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
        //public static void PrintCages()
        //{
        //    var dbContext = new HamsterDBContext();
        //    var hamsters = dbContext.Hamsters.OrderBy(h => h.CageId);
        //    foreach (var hamster in hamsters)
        //    {
                
        //        Console.WriteLine($"{hamster.CageId} {hamster.Name} {hamster.Gender}");
        //    }
           
        //}
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
