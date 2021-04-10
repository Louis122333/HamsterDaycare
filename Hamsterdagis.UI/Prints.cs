using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis.UI
{
    public class Prints
    {
        public static void PrintHamsters()
        {
            var dbContext = new HamsterDBContext();
            var hamsters = dbContext.Hamsters.OrderBy(h => h.Name);
            
            foreach (var hamster in hamsters)
            {
               if (hamster.Gender == 'K')
               {
                    var gender = "Female";
                    Console.WriteLine($"Hamster: {hamster.Name}\nAge: {hamster.Age} months\nGender: {gender}\nOwner: {hamster.OwnerName}");
                    Console.WriteLine();
               }
               else
               {
                    var gender = "Male";
                    Console.WriteLine($"Hamster: {hamster.Name}\nAge: {hamster.Age} months\nGender: {gender}\nOwner: {hamster.OwnerName}");
                    Console.WriteLine();
               }
            }
        }
     
        public static void PrintMenuOptions()
        {
            Prints.PrintMenuPrompt();

            
            Console.WriteLine("1. Start simulation");
            Console.WriteLine("2. Print hamster info");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Enter an option: ");
        }
        public static void PrintMenuPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string prompt = @" _       __     __                             __                                    
| |     / /__  / /________  ____ ___  ___     / /_____                               
| | /| / / _ \/ / ___/ __ \/ __ `__ \/ _ \   / __/ __ \                              
| |/ |/ /  __/ / /__/ /_/ / / / / / /  __/  / /_/ /_/ /                              
|__/___/____/_/\___/\____/_/ /___/_/\___/   \__/_____/   

   / / / /___ _____ ___  _____/ /____  _____   / __ \____ ___  ___________ _________ 
  / /_/ / __ `/ __ `__ \/ ___/ __/ _ \/ ___/  / / / / __ `/ / / / ___/ __ `/ ___/ _ \
 / __  / /_/ / / / / / (__  ) /_/  __/ /     / /_/ / /_/ / /_/ / /__/ /_/ / /  /  __/
/_/ /_/\__,_/_/ /_/ /_/____/\__/\___/_/     /_____/\__,_/\__, /\___/\__,_/_/   \___/ 
                                                        /____/       ";
           
            Console.WriteLine(prompt);
            Console.ResetColor();
        }

       
    }

}
