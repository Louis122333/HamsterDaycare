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
       
    }
}
