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
       /*TODO - Fixa MoveToExercise() -> MoveFromExercise
        *       Hamstrarna sparas inte i ExerciseArea
        *       HashSet<Hamster> Fuckar
        */

        public HamsterDayCare()
        {

            //LoadHamsters();
            //CreateCages();
            //CreateExerciseArea();
            PlaceHamstersInCages();
            MoveToExercise('K');
            MoveFromExercise();
            MoveToExercise('M');
            MoveFromExercise();

            //MoveFromExercise();

        }
        
        public static bool CreateExerciseArea()
        {
            bool noData = false;
            var dbContext = new HamsterDBContext();
            if (!dbContext.ExerciseArea.Any())
            {
                noData = true;
                dbContext.ExerciseArea.Add(new ExerciseArea());
                dbContext.SaveChanges();
            }
            return noData;
        }
        public static void ClearHamsters()
        {
            var dbContext = new HamsterDBContext();
            
            var all = from h in dbContext.Hamsters select h;
            dbContext.Hamsters.RemoveRange(all);
            dbContext.SaveChanges();
        }
       
            
        public static bool CreateCages()
        {
            bool noData = false;
            var dbcontext = new HamsterDBContext();
            if (!dbcontext.Cages.Any())
            {
                noData = true;
                for (int i = 0; i < 10; i++)
                {
                    dbcontext.Cages.Add(new Cage());
                }
            }
            dbcontext.SaveChanges();
            return noData;
        }
        public static bool LoadHamsters()
        {
            bool noData = false;
            var dbContext = new HamsterDBContext();
            if (!dbContext.Hamsters.Any())
            {
                noData = true;
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

            return noData;
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
                       //hamster.ActivityLogs.Add(new ActivityLog(DateTime.Now, Activity.Caged));
                    }
                }
                else
                {
                    for (int i = 0; i < cage.Size; i++)
                    {
                        var hamster = maleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        //hamster.ActivityLogs.Add(new ActivityLog(DateTime.Now, Activity.Caged));
                    }
                }
            }
            dbContext.SaveChanges();
            return true;
        }
        public static void MoveToExercise(char gender)
        {
            var dbContext = new HamsterDBContext();
            var exerciseArea = dbContext.ExerciseArea.First();
            var hamsters = dbContext.Hamsters
                .Where(h => h.LastTimeExercised == null)
                .Where(g => g.Gender == gender).ToList();

            var exerciseQueue = new Queue<Hamster>();
            for (int i = 0; i < hamsters.Count; i++)
            {
                exerciseQueue.Enqueue(hamsters[i]);
            }
            if (exerciseQueue.Count > exerciseArea.Size)
            {
                for (int i = 0; i < exerciseArea.Size; i++)
                {
                    var hamster = exerciseQueue.Dequeue();
                    var cage = dbContext.Cages
                        .Where(c => c.CageId == hamster.CageId)
                        .First();
                    exerciseArea.Hamsters.Add(hamster);//
                    hamster.CageId = null;
                    hamster.ExerciseAreaId = exerciseArea.ExerciseAreaId;
                    hamster.LastTimeExercised = DateTime.Now;
                    //dbContext.SaveChanges();
                }
            }
            else if (exerciseQueue.Count == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    var hamster = exerciseQueue.Dequeue();
                    var cage = dbContext.Cages
                        .Where(c => c.CageId == hamster.CageId)
                        .First();
                    hamster.CageId = null;
                    hamster.ExerciseAreaId = exerciseArea.ExerciseAreaId;
                    hamster.LastTimeExercised = DateTime.Now;
                   // dbContext.SaveChanges();
                }
            }
        }
        public static void MoveFromExercise()
        {
            var dbContext = new HamsterDBContext();
            var exerciseArea = dbContext.ExerciseArea.First();
            var hamsters = dbContext.Hamsters
                .Where(h => h.CageId == null && h.ExerciseAreaId != null);
            var maleQueue = new Queue<Hamster>();
            var femaleQueue = new Queue<Hamster>();
            foreach (var hamster in hamsters)
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
                        hamster.ExerciseAreaId = null;
                        hamster.CageId = cage.CageId;

                    }
                }
                if (maleQueue.Count > 0)
                {
                    for (int i = 0; i < cage.Size; i++)
                    {
                        var hamster = maleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        hamster.ExerciseAreaId = null;
                        hamster.CageId = cage.CageId;
                    }
                }
            }
                
            //foreach (var hamster in dbContext.ExerciseArea)
            //{
            //    foreach (var fitHamster in hamster.Hamsters)
            //    {
            //        var cage = dbContext.Cages.First(c => c.Hamsters.Count < 3);
            //        cage.Hamsters.Add(fitHamster);
            //        fitHamster.CageId = cage.CageId;
            //        fitHamster.ExerciseAreaId = null;
                    

            //    }
            //}
            //dbContext.SaveChanges();
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
