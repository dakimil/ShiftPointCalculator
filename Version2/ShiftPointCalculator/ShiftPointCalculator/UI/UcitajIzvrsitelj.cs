using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal class UcitajIzvrsitelj : IIzvrsitelj
    {
        //ucitava korisnicki fajl
        public bool Izvrsi()
        {
            string? fileName;
            string filePath;
            Console.WriteLine("Unesite naziv i format fajla i ubacite ga u folder 'bin'");
            fileName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(fileName) ) {
                Console.WriteLine("Uneli ste prazno ime fajla");
                return false;
            }
            filePath = $"F:\\Daki\\GitHub\\ShiftPointCalculator\\Version2\\" +
                "ShiftPointCalculator\\ShiftPointCalculator\\bin\\" + fileName;

            bool fileExists = File.Exists(filePath);

            if(!fileExists)
            {
                Console.WriteLine($"File {filePath} nije pronadjen");

                return false;
            }


            string[] lines = File.ReadAllLines(filePath);

            return false;
        }
    }
}
