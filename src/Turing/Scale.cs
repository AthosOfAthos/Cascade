using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.Turing
{
    public class Scale
    {
        public Scale(int activatorRadius, int inhibitorRadius, double increment)
        {
            ActivatorRadius = activatorRadius;
            InhibitorRadius = inhibitorRadius;
            Increment = increment;
        }

        public int ActivatorRadius { get; set; }
        public int InhibitorRadius { get; set; } 
        public double Increment { get; set; }
        
    }
}
