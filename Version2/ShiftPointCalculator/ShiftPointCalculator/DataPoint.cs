using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator
{
    internal class DataPoint
    {
        public decimal MomentNaTocku { get; set; }
        public decimal MomentMotoraPriObrtajima { get; set; }
        public decimal BrojObrtajaMotora { get; set; }
        public decimal PrenosniOdnosMenjaca { get; set; }
        public int RedniBrojStepenaPrenosaMenjaca { get; set; }
        public decimal BrzinaVozila { get; set; }


    }
}
