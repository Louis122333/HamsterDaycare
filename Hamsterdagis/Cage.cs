﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public class Cage
    {
        public int CageId { get; set; }
        public int Size = 3;
        public int? HamsterId { get; set; }
        public Cage()
        {
            Hamsters = new HashSet<Hamster>();
        }
        public ICollection<Hamster> Hamsters { get; set; }
    }
}
