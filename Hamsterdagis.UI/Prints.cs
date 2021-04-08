using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis.UI
{
    public class Prints
    {
        public static void PrintCages()
        {
            var dbContext = new HamsterDBContext();
            var hamsters = dbContext.Hamsters.OrderBy(h => h.CageId);
            var cages = dbContext.Cages.OrderBy(c => c.CageId);
           
            foreach (var hamster in hamsters)
            {

                Console.WriteLine($"{hamster.CageId} {hamster.Name} {hamster.Gender}");
            }

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
