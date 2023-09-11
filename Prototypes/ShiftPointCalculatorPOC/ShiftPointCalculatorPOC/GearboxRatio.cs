using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculatorPOC
{
    public class GearboxRatio
    {
        public GearboxRatio(
            int gearNumber, 
            decimal gearRatio)
        {
            GearNumber = gearNumber;
            GearRatio = gearRatio;
        }

        public int GearNumber { get; }

        public decimal GearRatio { get; }
    }
}
