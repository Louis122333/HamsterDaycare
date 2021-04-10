using System;
using Hamsterdagis;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hamsterdagis.UI
{
    public class UserInterface
    {
        private static int _printSpeed;
        public static void SimulationMenu()
        {
            Console.Write("Enter how many days you want to simulate: ");
            _ = int.TryParse(Console.ReadLine(), out int days);
            if (days == 0 || days > 10)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                SimulationMenu();
            }
            Console.Write("Enter the tick speed (ms): ");
            _ = int.TryParse(Console.ReadLine(), out int tickSpeed);
            if (tickSpeed < 1 || tickSpeed > 3000)
            {
                Console.WriteLine("Invalid tick speed");
                Console.WriteLine("Press any key to return");
                Console.ReadKey();
                SimulationMenu();
            }
            Console.Write("Enter the print speed (ms): ");
            _ = int.TryParse(Console.ReadLine(), out int printSpeed);
            _printSpeed = printSpeed;
            if (printSpeed < tickSpeed)
            {
                Console.WriteLine("Print speed must be higher or equal to tick speed");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                SimulationMenu();
            }
            else
            {
                Console.Clear();
                var simulation = new Simulation(tickSpeed, days);
                simulation.RunSimulation();
                StartSim();
                PrintActivity();
                
            }
        }
        public static void MainMenu()
        {
            Prints.PrintMenuOptions();
            bool showmenu = true;
            while (showmenu)
            {
                
                int menuChoice = GetMenuChoice();
                Console.Clear();
                switch (menuChoice)
                {
                    case 1:
                        SimulationMenu();
                        break;
                    case 2:
                        Prints.PrintHamsters();
                        Console.Write("Press any key to return: ");
                        Console.ReadKey();
                        Console.Clear();
                        Prints.PrintMenuOptions();
                        break;
                    case 3:
                        Console.WriteLine("Bye!");
                        Console.Write("Press any key to exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }
        private static void PrintActivity()
        {
            var dbContext = new HamsterDBContext();
            var counter = 0;
            var printingActivity = true;
            while (printingActivity)
            {
                var date = Simulation.Date;
                Thread.Sleep(_printSpeed);
                var activityLogs = dbContext.ActivityLogs.Where(a => a.Timestamp == date).OrderBy(h => h.Hamster.Name);
                if (activityLogs.Count() > 1)
                {
                   
                    Console.SetCursorPosition(20, 5);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tick: {counter++} Date: {date}");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.SetCursorPosition(20, 7);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Name   \tActivity   \tTimes Exercised   \tTime waited for first exercise");
                    Console.ResetColor();
                    Console.WriteLine();

                    //Sets the condition for the cursor position, iterates +1 for every hamster
                    var hamsterCounter = 8;
                    
                    foreach (var activity in activityLogs)
                    {
                        int timesExercised = activity.Hamster.ActivityLogs.Where(a => a.Activity == Activity.Exercising && a.Timestamp <= date).Count() / 10;
                        var minutes = ExerciseWaitTime(activity.Hamster, date);
                        if (activity.Hamster.Gender == 'K')
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.SetCursorPosition(20, hamsterCounter);
                            Console.WriteLine("{0,-10}\t{1,-12}\t{2,-22}\t{3} minutes", activity.Hamster.Name, activity.Activity, timesExercised, minutes);
                            Console.ResetColor();
                        }
                        if (activity.Hamster.Gender == 'M')
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.SetCursorPosition(20, hamsterCounter);
                            
                            Console.WriteLine("{0,-10}\t{1,-12}\t{2,-22}\t{3} minutes", activity.Hamster.Name, activity.Activity, timesExercised, minutes);
                            Console.ResetColor();
                        }
                        hamsterCounter++;
                    }
                    
                    var hamsters = dbContext.Hamsters.Where(h => h.CageId != null || h.ExerciseAreaId != null).Count();
                    var exercising = dbContext.Hamsters.Where(h => h.ExerciseArea != null).Count();

                    Console.SetCursorPosition(20, 40);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number of hamsters in Daycare: {hamsters}");
                    Console.SetCursorPosition(20, 41);
                    Console.WriteLine($"Number of hamsters in Exercise Area: {exercising}");
                    Console.ResetColor();
                }
            }
        }
       
       
        private static int ExerciseWaitTime(Hamster hamster, DateTime date)
        {
            var activityList = hamster.ActivityLogs.Where(h => h.Activity == Activity.Exercising).ToList().OrderBy(a => a.Timestamp);
            var arrivalTime = new TimeSpan(7, 0, 0);
            if (activityList.Any())
            {
                var waitTime = activityList.First().Timestamp.TimeOfDay - arrivalTime;
                var totalwaitTime = waitTime.TotalMinutes;
                return (int)totalwaitTime;
            }
            else
            {
                var waiting = date.TimeOfDay - arrivalTime;
                var totalwaiting = waiting.TotalMinutes;
                return (int)totalwaiting;
            }
        }

        private static int GetMenuChoice()
        {
            int userMenuChoice = 0;
            bool validChoice = false;
            while (validChoice == false)
            {
                validChoice = int.TryParse(Console.ReadLine(), out userMenuChoice);
                if (validChoice == false)
                {
                    Console.WriteLine("Please input a valid number: ");
                }
                if (userMenuChoice < 1 || userMenuChoice > 3)
                {
                    Console.WriteLine("Please choose an option between 1 and 3: ");
                    validChoice = false;
                }
            }
            return userMenuChoice;
        }
        private static void StartSim()
        {
            Console.Write("Press "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("<Enter>"); Console.ResetColor(); Console.Write(" to start the simulation: ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                
                Console.SetCursorPosition(5, 5);
                Console.Write("Loading: {0}% ", i);
                while (i == 14 || i == 35)
                {
                    Thread.Sleep(1500);
                    Console.Write("Separating boys from girls..");
                    break;
                }
                while (i == 59 || i == 99)
                {
                    Thread.Sleep(1500);
                    Console.Write("Throwing hamsters in cages..");
                    break;
                }
            }
            Console.Write("Complete! Starting simulation...");
            Thread.Sleep(1000);
            Console.Clear();
        }
      




    }

}

