using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator
{
    internal class UlazniPodaci
    {
        public decimal PoluprecnikTocka { get; set; }

        public decimal PrenosniOdnosUDiferncijalu { get; set; }

        public List<StepenPrenosaMenjaca> StepeniPrenosaMenjaca = new List<StepenPrenosaMenjaca>();

        public List<MomentMotora> MomentiMotora = new List<MomentMotora>();
    }
}
