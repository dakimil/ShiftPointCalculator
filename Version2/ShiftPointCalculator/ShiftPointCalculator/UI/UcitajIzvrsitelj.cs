using ShiftPointCalculator.BusinessLogic;
using ShiftPointCalculator.DataAcces;
using System.Reflection;

namespace ShiftPointCalculator.UI
{
    internal class UcitajIzvrsitelj : IIzvrsitelj
    {
        //ucitava korisnicki fajl
        public bool Izvrsi()
        {
            string? fileName;
            string filePath;
            Console.WriteLine("Unesite naziv i format fajla i ubacite ga u folder 'bin'");//prilagodi
            fileName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Uneli ste prazno ime fajla");
                return false;
            }

            /*
            filePath = $"C:\\Users\\Daki\\source\\repos\\ShiftPointCalculator\\Version2\\ShiftPointCalculator" +
                $"\\ShiftPointCalculator\\bin\\Debug\\" +
                $"net6.0\\VehicleData\\" + fileName;
            */

            Assembly? asm = Assembly.GetEntryAssembly();
            
            string? asmPath = asm!.Location;

            string asmFolder = Path.GetDirectoryName(asmPath!);

            filePath = Path.Combine(asmFolder!, "VehicleData", fileName);

            bool fileExists = File.Exists(filePath);

            if (!fileExists)
            {
                Console.WriteLine($"File {filePath} nije pronadjen");

                return false;
            }


            string[] lines = File.ReadAllLines(filePath);
            UlazniPodaci up = Parser.Parsiraj(lines);
            UlazniPodaciVozilaRepository.Save(up);
            return false;
        }
    }
}
