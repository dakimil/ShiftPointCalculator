using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.BusinessLogic
{
    public class StepenPrenosaMenjaca //bilo je internal class ali je promenjeno zbog metoda GetByVoziloId u StepenPrenosaMenjacaProvider
    {
        public int RedniBrojStepenaPrenosa { get; set; }

        public decimal PrenosniOdnos { get; set; }
    }
}
