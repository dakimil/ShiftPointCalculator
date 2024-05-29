using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.BusinessLogic
{
    public class UlazniPodaci
    {
        public string NazivVozila { get; set; }

        public int PoluprecnikTocka { get; set; }

        public decimal GlavniPrenos { get; set; }

        public List<StepenPrenosaMenjaca> StepeniPrenosaMenjaca = new List<StepenPrenosaMenjaca>();

        public List<MomentMotora> MomentiMotora = new List<MomentMotora>();
    }
}
