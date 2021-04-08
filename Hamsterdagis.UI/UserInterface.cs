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
        public static void HamsterMenu()
        {
            Prints.PrintMenuPrompt();

            Console.Write("Enter how many days you want to simulate: ");
            _ = int.TryParse(Console.ReadLine(), out int days);
            if (days == 0 || days > 10)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                HamsterMenu();
            }
            Console.Write("Enter the tick speed (ms): ");
            _ = int.TryParse(Console.ReadLine(), out int tickSpeed);
            if (tickSpeed < 1 || tickSpeed > 3000)
            {
                Console.WriteLine("Invalid tick speed");
                Console.WriteLine("Press any key to return");
                Console.ReadKey();
                HamsterMenu();
            }
            Console.Write("Enter the print speed (ms): ");
            _ = int.TryParse(Console.ReadLine(), out int printSpeed);
            _printSpeed = printSpeed;
            if (printSpeed < tickSpeed)
            {
                Console.WriteLine("Print speed must be higher or equal to tick speed");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                HamsterMenu();
            }
            else
            {
                var simulation = new Simulation(tickSpeed, days);
                simulation.RunSimulation();
                Console.Write("Press any key to begin the simualtion: ");
                Console.ReadKey();
                PrintActivity();
            }
        }
        public static void PrintActivity()
        {
            var dbContext = new HamsterDBContext();
            var thisDate = DateTime.Now;
            var date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, 7, 0, 0);
            var Counter = 0;
            while (true)
            {
                Thread.Sleep(_printSpeed);
                var activityLogs = dbContext.ActivityLogs.Where(a => a.Timestamp == date).OrderBy(h => h.Hamster.Name);
                if (activityLogs.Count() > 1)
                {
                    Console.WriteLine($"Tick: {Counter++} Date: {date}");
                    Console.WriteLine();
                    Console.WriteLine($"Name   Activity");
                    date = date.AddMinutes(6);
                    var tickCounter = 2;
                    foreach (var activity in activityLogs)
                    {

                        if (tickCounter % 2 == 1)
                        {
                            Console.WriteLine($"{activity.Hamster.Name}  {activity.Activity}");
                            tickCounter++;
                        }
                        else if (tickCounter % 2 == 0)
                        {
                            Console.WriteLine($"{activity.Hamster.Name} {activity.Activity}");
                            tickCounter++;
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
       
    }
}
