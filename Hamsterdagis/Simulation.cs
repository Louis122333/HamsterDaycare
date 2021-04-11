using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public class Simulation
    {
        public int Counter { get; set; }
        public static DateTime Date { get; set; }
        public int NumOfDays { get; set; }
        public int DaysGone { get; set; }
        public int SleepTimer { get; set; }
        public Simulation(int sleepTimer, int numOfDays)
        {
            Counter = 0;
            var thisDate = DateTime.Now;
            Date = new DateTime(year: thisDate.Year, month: thisDate.Month, day: thisDate.Day, hour: 7, minute: 0, second: 0);
            SleepTimer = sleepTimer;
            NumOfDays = numOfDays;
            DaysGone = 0;
        }
        public void OnCounter()
        {
            if (Counter == 0)
            {
                HamsterDayCare.MoveToExercise('K');
            }
            if (Counter == 10)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('K');
            }
            if (Counter == 20)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('K');
            }
            if (Counter == 30)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('M');
            }
            if (Counter == 40)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('M');
            }
            if (Counter == 50)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('M');
            }
            if (Counter == 60)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('K');
            }
            if (Counter == 70)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('K');
            }
            if (Counter == 80)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('M');
            }
            if (Counter == 90)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.MoveToExercise('M');
            }
            if (Counter == 100)
            {
                HamsterDayCare.MoveFromExercise();
                HamsterDayCare.SendHamstersHome();
            }
            ActivityTracker();
        }
        private static void ActivityTracker()
        {
            using (var dbContext = new HamsterDBContext())
            {
                foreach (var cage in dbContext.Cages)
                {
                    foreach (var hamster in cage.Hamsters)
                    {
                        hamster.ActivityLogs.Add(new ActivityLog(Date, Activity.Caged));
                    }
                }
                foreach (var hamster in dbContext.ExerciseArea)
                {
                    foreach (var exHamster in hamster.Hamsters)
                    {
                        exHamster.ActivityLogs.Add(new ActivityLog(Date, Activity.Exercising));
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public void RunSimulation()
        {
            Task.Run(() =>
            {
                while (NumOfDays > DaysGone)
                {
                    Thread.Sleep(SleepTimer);
                    OnCounter();
                    Counter++;
                    Date = Date.AddMinutes(6);
                    if (Counter == 101)
                    {
                        DaysGone++;
                        Counter = 0;
                        var thisDate = DateTime.Now;
                        Date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, hour: 7, minute: 0, second: 0).AddDays(DaysGone);

                        // * Uncomment the two rows at line 126-127 if you want to simulate more than 1 day.
                        // * The Hamster.CageId doesn't reset to null when simulating 2 days or more.
                        // * Any other additional data is correct /dev

                        //var dbContext = new HamsterDBContext();
                        //HamsterDayCare.PlaceHamstersInCages();
                    }
                }
            });
        }
    }
}