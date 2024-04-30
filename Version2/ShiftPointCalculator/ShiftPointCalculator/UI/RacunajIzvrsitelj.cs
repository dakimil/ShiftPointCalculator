using ShiftPointCalculator.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.UI
{
    internal class RacunajIzvrsitelj : IIzvrsitelj
    {
        //Racuna za dati ID
        public bool Izvrsi()
        {
            Console.WriteLine("Unesite ID zeljenog vozila");
            int id = Convert.ToInt32(Console.ReadLine());
            UlazniPodaci up = UlazniPodaciVozilaRepository.GetByVoziloId(id);
            Vozilo kola = new Vozilo(id, up);

            kola.Racunaj();

            foreach(var k in kola.MomentiNaTockovimaZaSveBrzineVozila)
            {
                Console.Write($"Brzina: {k.Key};");
                foreach(var v in k.Value.DataPoints)
                {
                    Console.Write($"{v.Value.MomentNaTocku};");
                }
                Console.WriteLine();
            }//sredi formatiranje
            return false;
        }

    }
}
