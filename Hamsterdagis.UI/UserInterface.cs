﻿using System;
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
                var simulation = new Simulation(tickSpeed, days);
                simulation.RunSimulation();
                Console.Write("Press any key to begin the simualtion...");
                Console.ReadKey();
                Console.Clear();
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
        public static void PrintActivity()
        {
            var dbContext = new HamsterDBContext();
            var counter = 0;

            while (true)
            {
                var date = Simulation.Date;
                Thread.Sleep(_printSpeed);
                var activityLogs = dbContext.ActivityLogs.Where(a => a.Timestamp == date).OrderBy(h => h.Hamster.Name);
                if (activityLogs.Count() > 1)
                {
                    Console.WriteLine($"Tick: {counter++} Date: {date}");
                    Console.WriteLine();
                    Console.WriteLine($"Name   Activity");
                    Console.WriteLine();
                    var hamsterCounter = 0;
                    foreach (var activity in activityLogs)
                    {
                        Console.WriteLine($"{activity.Hamster.Name}  {activity.Activity}");
                        hamsterCounter++;
                    }
                    Console.WriteLine();
                }
            }
        }
       
        public static void PrintActivity2()
        {
            var dbContext = new HamsterDBContext();
            var thisDate = DateTime.Now;
            var date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, 7, 0, 0);
            var counter = 0;
            while (true)
            {
                Thread.Sleep(_printSpeed);
                var activityLogs = dbContext.ActivityLogs.Where(a => a.Timestamp == date).OrderBy(h => h.Hamster.Name).ThenBy(h => h.Activity).ToList();
                if (activityLogs.Count == 60 && counter == 0)
                {
                    Console.WriteLine($"Tick {counter++} {date}");
                    Console.WriteLine();
                    Console.WriteLine("Name     Activity");
                    Console.WriteLine();
                    var counter2 = 2;
                    foreach (var activity in activityLogs)
                    {
                        if (counter2 % 2 == 0)
                        {
                            int timesExercised = activity.Hamster.ActivityLogs.Where(a => a.Activity == Activity.Exercising && a.Timestamp <= date).Count() / 10;
                            var minutes = ExerciseWaitTime(activity.Hamster, date);
                            Console.WriteLine($"{activity.Hamster.Name} {activity.Activity} {minutes}");
                        }
                        else
                        {
                            Console.WriteLine($"{activity.Activity}");
                        }
                        counter2++;
                    }
                    if (date.Hour == 17)
                    {
                        date = date.AddHours(14);
                        counter = 0;
                    }
                    else
                    {
                        date = date.AddMinutes(6);
                    }
                    Console.WriteLine();
                }
                else if (activityLogs.Count > 1 && counter > 0)
                {
                    Console.WriteLine($"Tick {counter++} {date}");
                    Console.WriteLine();
                    Console.WriteLine("Name     Activity");
                    foreach (var activity in activityLogs)
                    {
                        int timesExercised = activity.Hamster.ActivityLogs.Where(a => a.Activity == Activity.Exercising && a.Timestamp <= date).Count() / 10;
                        var hours = ExerciseWaitTime(activity.Hamster, date);
                        Console.WriteLine($"{activity.Hamster.Name} {activity.Activity} {hours}");
                    }
                    if (date.Hour == 17)
                    {
                        date = date.AddHours(14);
                        counter = 0;
                    }
                    else
                    {
                        date = date.AddMinutes(6);
                    }
                    Console.WriteLine();

                }
            }

        }
        public static int ExerciseWaitTime(Hamster hamster, DateTime date)
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

        public static int GetMenuChoice()
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
      




    }

}

