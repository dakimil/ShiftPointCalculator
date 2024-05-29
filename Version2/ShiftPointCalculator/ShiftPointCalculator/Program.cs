using ShiftPointCalculator.DataAcces;
using ShiftPointCalculator.Repositories;
using ShiftPointCalculator.UI;
using System;
using System.Globalization;
using System.Text;

namespace ShiftPointCalculator
{
    internal class Program
    {
        // test commit
        

        static void Main(string[] args)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            // diskutuj "." i ","
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            bool izadji = false;

            while (!izadji)
            {
                Komande? izabranaKomanda = null;
                while (true)
                {
                    IspisiGlavniMeni();
                    izabranaKomanda = OcitajKomandu();

                    if (izabranaKomanda != null)
                    {
                        break;
                    }
                }

                Console.WriteLine($"Izabrali ste: {izabranaKomanda}");

                IIzvrsitelj izvrsitelj = FabrikaIzvrsitelja.VratiIzvrsitelja(
                    komanda: izabranaKomanda.Value);

                Console.WriteLine($"Ivrsitelj za komandu {izabranaKomanda} je {izvrsitelj.GetType().Name}");

                izadji = izvrsitelj.Izvrsi();
            }

            return;
        }

        private static Komande? OcitajKomandu()
        {
            string? commandString = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(commandString))
            {
                Console.WriteLine("Niste uneli komandu");

                return null;
            }


            bool isNumber = Int32.TryParse(
                commandString,
                out var commandInt);

            if (!isNumber)
            {
                Console.WriteLine("Unesite broj komande");
                return null;
            }

            if (!Enum.IsDefined(typeof(Komande), commandInt))
            {
                Console.WriteLine("Niste uneli poznati broj komande");
                return null;
            }

            Komande chosenCommand = (Komande)commandInt;

            return chosenCommand;
        }

        private static void IspisiGlavniMeni()
        {
            IEnumerable<Komande> commands = Enum.GetValues<Komande>();

            Console.WriteLine("Izaberi:");

            foreach (var command in commands)
            {
                Console.WriteLine($"{(int)command} - {command}");
            }
        }
    }
}