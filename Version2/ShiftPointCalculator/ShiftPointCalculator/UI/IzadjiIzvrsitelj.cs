using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal class IzadjiIzvrsitelj : IIzvrsitelj
    {
        public bool Izvrsi()
        {
            // reci glavnom programu da izadje
            Environment.Exit(0);
            return true;
        }
    }
}
