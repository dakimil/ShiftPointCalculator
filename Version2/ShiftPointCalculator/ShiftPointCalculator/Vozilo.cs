using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ovde racunamo momente na tockovima za sve brzine vozila
namespace ShiftPointCalculator
{
    public class Vozilo
    {
        public UlazniPodaci UlazniPodaci { get; set; }
        /// <summary>
        /// Kljuc za pretragu je brzina vozila
        /// </summary>
        public Dictionary<int, MomentiNaTockovimaZaBrzinuVozila> MomentiNaTockovimaZaSveBrzineVozila { get; } =
        new Dictionary<int, MomentiNaTockovimaZaBrzinuVozila>();
    }
}
