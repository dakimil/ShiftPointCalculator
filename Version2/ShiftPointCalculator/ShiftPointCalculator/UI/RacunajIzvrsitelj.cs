﻿using ShiftPointCalculator.BusinessLogic;
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

            ConsoleColor defaultColor = Console.ForegroundColor;

            foreach (var k in kola.MomentiNaTockovimaZaSveBrzineVozila)
            {
                string brzinaString = String.Format("{0, 3}", $"{k.Key}");
                //s += String.Format("{0,20}", $"|Brzina: {k.Key} km/h;| ");
                string brzinaMenjaca = String.Format("{0,20}", $"|Brzina: {brzinaString} km/h;| ");
                Console.Write(brzinaMenjaca);
                foreach (KeyValuePair<int, DataPoint> v in k.Value.DataPoints)
                {
                    string dataPoint = String.Empty;

                    if (v.Value.MomentNaTocku != 0)
                    {
                        string momentString = $"{Math.Round(v.Value.MomentNaTocku, 2)}";
                        string brojObrtajaString = $"{Math.Floor(v.Value.BrojObrtajaMotora)}";
                        dataPoint = String.Format("{0,20}", $"{momentString} Nm/{brojObrtajaString};| ");
                    }
                    else
                    {
                        dataPoint = String.Format("{0,20}", "ND;| ");
                    }


                    //s += String.Format("{0,15}", $"{Math.Round(v.Value.MomentNaTocku, 2)} Nm;| ");

                    if (v.Value == k.Value.DataPointWithMaxMoment)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.ForegroundColor = defaultColor;
                    }

                    Console.Write(dataPoint);

                    Console.ForegroundColor = defaultColor;
                }
                // new row
                Console.WriteLine();
            }//sredi formatiranje

            Console.ForegroundColor = defaultColor;

            return false;
        }

    }
}
