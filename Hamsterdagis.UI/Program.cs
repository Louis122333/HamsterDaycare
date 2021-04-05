using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Hamsterdagis.UI
{
    class Program
    {
       

        static void Main(string[] args)
        {
            var _context = new HamsterDBContext();
            _context.Database.EnsureCreated();
            HamsterDayCare h = new HamsterDayCare();

            //Prints.PrintCages();
            
            





            Console.ReadLine();

        }

    }
}
       

    

