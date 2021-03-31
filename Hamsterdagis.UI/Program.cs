using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Hamsterdagis.UI
{
    class Program
    {
        private static HamsterDBContext _context = new HamsterDBContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            UserInterface userInterface = new UserInterface();
            //userInterface.Menu();
            
            
            Console.ReadLine();

        }
        
        
    }
   
}
       

    

