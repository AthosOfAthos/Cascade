using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.Turing
{
    public class Scale
    {
        public Scale(double activatorRadius, double inhibitorRadius, double increment)
        {
            ActivatorRadius = activatorRadius;
            InhibitorRadius = inhibitorRadius;
            Increment = increment;
        }

        public double ActivatorRadius { get; set; }
        public double InhibitorRadius { get; set; } 
        public double Increment { get; set; }
        
    }
}
