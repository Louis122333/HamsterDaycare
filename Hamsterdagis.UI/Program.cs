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
            _ = new HamsterDayCare();
            UserInterface.MainMenu();
            Console.ReadLine();
        }
    }
}
       

    

