using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal interface IIzvrsitelj
    {
        /// <summary>
        /// Izvrsava komandu.
        /// </summary>
        /// <returns>
        /// true ako program treba da se zavrsi posle izvrsenja komande (npr. ako izvrsi Komande.Izadji)
        /// </returns>
        bool Izvrsi();
    }
}
