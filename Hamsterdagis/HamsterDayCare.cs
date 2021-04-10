﻿using System;
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
        /*TODO - 
         *       Skapa metod som räknar hur lång tid hamstrarna har väntat på Exercise
         *       
         *       Skapa en FinalLog som skriver ut info vid dagens slut //eller Sökfunktion på specifik hamster
         *       som skriver ut all info
         *       
         *       TOMBU KL 16.00
         *       ÖL KL 14:30
         *       
         *    
         */

        public HamsterDayCare()
        {
            ClearLogs();
            ResetData();
            InitializeDB();
        }

        #region Startup Methods
        private static void InitializeDB()
        {
            LoadHamsters();
            CreateCages();
            CreateExerciseArea();
            PlaceHamstersInCages();
        }
        private static bool ResetData()
        {
            bool noData = false;
            var dbContext = new HamsterDBContext();
            var thisDate = DateTime.Now;
            var Date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, 7, 0, 0);
            if (dbContext.Hamsters.Any())
            {
                foreach (var hamster in dbContext.Hamsters)
                {
                    hamster.CageId = null;
                    hamster.ExerciseAreaId = null;
                    hamster.LastTimeExercised = null;
                    hamster.ArrivalTime = Date;
                }
            }
            dbContext.SaveChanges();
            return noData;
        }
        private static void ClearLogs()
        {
            var dbContext = new HamsterDBContext();
            if (dbContext.ActivityLogs.Any())
            {
                foreach (var log in dbContext.ActivityLogs)
                {
                    dbContext.Remove(log);
                }
            }
            dbContext.SaveChanges();
        }
        private static bool CreateExerciseArea()
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
        private static bool CreateCages()
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
        private static bool LoadHamsters()
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
        #endregion

        #region Hamster Simulation Methods
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
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = femaleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        var thisDate = DateTime.Now;
                        var Date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, 6, 59, 59);
                        hamster.ActivityLogs.Add(new ActivityLog(Date, Activity.Arrival));
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = maleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        var thisDate = DateTime.Now;
                        var Date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, 6, 59, 59);
                        hamster.ActivityLogs.Add(new ActivityLog(Date, Activity.Arrival));
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
                .OrderBy(h => h.LastTimeExercised)
                .Where(g => g.Gender == gender).ToList();

            var exerciseQueue = new Queue<Hamster>();
            for (int i = 0; i < hamsters.Count; i++)
            {
                exerciseQueue.Enqueue(hamsters[i]);
            }
            if (exerciseQueue.Count > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    var hamster = exerciseQueue.Dequeue();
                    var cage = dbContext.Cages
                        .Where(c => c.CageId == hamster.CageId)
                        .First();
                    exerciseArea.Hamsters.Add(hamster);
                    hamster.CageId = null;
                    hamster.ExerciseAreaId = exerciseArea.ExerciseAreaId;
                    hamster.LastTimeExercised = Simulation.Date;
                    dbContext.SaveChanges();
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
                    hamster.LastTimeExercised = Simulation.Date;
                    dbContext.SaveChanges();
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
            var query = dbContext.Cages.Where(c => c.Hamsters.Count < 3);
            foreach (var cage in query)
            {
                if (femaleQueue.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = femaleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        hamster.CageId = cage.CageId;
                        hamster.ExerciseAreaId = null;
                    }
                }
                else if (maleQueue.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = maleQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                        hamster.CageId = cage.CageId;
                        hamster.ExerciseAreaId = null;
                    }
                }
            }
            dbContext.SaveChanges();
        }
        public static void SendHamstersHome()
        {
            using (var dbContext = new HamsterDBContext())
            {
                dbContext.Cages.ToList()
                                   .ForEach(c => c.Hamsters.Clear());
                dbContext.Hamsters.ToList()
                                  .ForEach(h => h.ActivityLogs.Add(new ActivityLog(Simulation.Date, Activity.Departed)));
                dbContext.SaveChanges();
            }
        }
        #endregion

    }
}
