using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Hamsterdagis
{
    public class HamsterDBContext : DbContext
    {
       public DbSet<Hamster> Hamsters { get; set; }
       public DbSet<Cage> Cages { get; set; }
       public DbSet<ExerciseArea> ExerciseArea { get; set; }
       public DbSet<ActivityLog> ActivityLogs { get; set; }

        // Don't forget to change the server name when testing this application for yourself.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DAT1QGR\\SQLEXPRESS;Database=advLouisHeadlam;Trusted_Connection=True;MultipleActiveResultSets=True;").UseLazyLoadingProxies();   
        }
    }
}
