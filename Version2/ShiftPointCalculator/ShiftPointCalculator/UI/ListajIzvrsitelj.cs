using ShiftPointCalculator.DataAcces;
using ShiftPointCalculator.QueryResults;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            ConsoleColor resultColor = ConsoleColor.Cyan;
            ConsoleColor defaultColor = Console.ForegroundColor;

            foreach (var item in VoziloDataProvider.GetAll()) 
            {
                //Console.WriteLine($"Naziv vozila:{item.NazivVozila}; Id vozila: {item.Id}");
                Console.Write("Naziv vozila: ");
                Console.ForegroundColor = resultColor;
                Console.Write(item.NazivVozila);
                Console.ForegroundColor = defaultColor;
                Console.Write(" Id vozila: ");
                Console.ForegroundColor = resultColor;
                Console.Write(item.Id);
                Console.WriteLine();
                Console.ForegroundColor = defaultColor;
            }
            return false;
        }
    }
}
