using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator
{
    internal class Parser
    {
        public static UlazniPodaci Parsiraj(IEnumerable<string> linije)
        {
            const string separator = "-------------";
            UlazniPodaci up = new UlazniPodaci();
            int br_separatora = 0, stepen = 1;
            foreach (string linija in linije)
            {
                if (linija.Equals(separator))
                {
                    br_separatora++;
                    continue;
                }

                if (br_separatora == 0)
                {
                    up.NazivVozila = linija;
                }
                else if (br_separatora == 1)
                {
                    up.PoluprecnikTocka = Convert.ToInt32(linija);
                }
                else if (br_separatora == 2)
                {
                    up.GlavniPrenos = Convert.ToDecimal(linija);
                }
                else if (br_separatora == 3)
                {
                    StepenPrenosaMenjaca spm = new StepenPrenosaMenjaca();
                    spm.RedniBrojStepenaPrenosa = stepen;
                    spm.PrenosniOdnos = Convert.ToDecimal(linija);
                    up.StepeniPrenosaMenjaca.Add(spm);
                    stepen++;
                }
                else
                {
                    MomentMotora mm = new MomentMotora();
                    string[] pom = linija.Split('\t');
                    mm.BrojObrtaja = Convert.ToInt32(pom[0]);
                    mm.MomentPriObrtajima = Convert.ToDecimal(pom[1]);
                    up.MomentiMotora.Add(mm);
                }
            }

            return up;
        }
    }
}
