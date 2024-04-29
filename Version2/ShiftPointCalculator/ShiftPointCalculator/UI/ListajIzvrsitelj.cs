using ShiftPointCalculator.DataAcces;
using ShiftPointCalculator.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal class ListajIzvrsitelj : IIzvrsitelj
    {
        //Stampa listu vozila sa id
        public bool Izvrsi()
        {
            foreach (var item in VoziloDataProvider.GetAll()) 
            { 
                Console.WriteLine($"Naziv vozila:{item.NazivVozila}; Id vozila: {item.Id}");
            }
            return false;
        }
    }
}
