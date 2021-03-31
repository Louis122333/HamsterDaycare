using System;
using Hamsterdagis;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis.UI
{
    public class UserInterface
    {
        
        public void Menu()
        {
            
            HamsterDayCare h = new HamsterDayCare();
            HamsterDayCare.ReadFile();
            //HamsterDayCare.AddHamsters();
            HamsterDayCare.PrintHamsters();
            
            
            //HamsterDayCare.PrintHamsters();


        }
    }
}
