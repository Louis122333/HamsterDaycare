using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public class ExerciseArea
    {
        public int ExerciseAreaId { get; set; }
        public ExerciseArea()
        {
            Hamsters = new HashSet<Hamster>();
        }
        public virtual ICollection<Hamster> Hamsters { get; set; }
    }
}
