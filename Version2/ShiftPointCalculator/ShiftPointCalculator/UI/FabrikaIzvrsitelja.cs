using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal static class FabrikaIzvrsitelja
    {
        public static IIzvrsitelj VratiIzvrsitelja(Komande komanda)
        {
            switch(komanda)
            {
                case Komande.Izadji:
                    return new IzadjiIzvrsitelj();
                default:
                    throw new Exception($"Nepoznata komanda {komanda}");
            }
        }
    }
}
