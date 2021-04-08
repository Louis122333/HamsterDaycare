using System;
using System.Collections.Generic;

namespace Hamsterdagis
{
    public class Hamster
    {
        public int HamsterId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public string OwnerName { get; set; }
        public int? CageId { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? LastTimeExercised { get; set; }
       
        public virtual Cage Cage { get; set; }
        public int? ExerciseAreaId { get; set; }
        public virtual ExerciseArea ExerciseArea { get; set; }
        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }


    }
}
