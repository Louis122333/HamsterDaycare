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
            
            HamsterDayCare h = new HamsterDayCare();

            //HamsterDayCare.ReadFile();
            //HamsterDayCare.PrintHamsters();
            var _context = new HamsterDBContext();
            _context.Database.EnsureCreated();





            Console.ReadLine();

        }

    }
}
       

    

