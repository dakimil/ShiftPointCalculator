using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ovde racunamo momente na tockovima za sve brzine vozila
namespace ShiftPointCalculator.BusinessLogic
{
    public class Vozilo
    {
        public Vozilo(int id, UlazniPodaci up)
        {
            Id = id;
            UlazniPodaci = up;
        }

        public int Id { get; private set; }

        public UlazniPodaci UlazniPodaci { get; private set; }
        /// <summary>
        /// Kljuc za pretragu je brzina vozila
        /// </summary>
        public Dictionary<int, MomentiNaTockovimaZaBrzinuVozila> MomentiNaTockovimaZaSveBrzineVozila { get; } =
        new Dictionary<int, MomentiNaTockovimaZaBrzinuVozila>(); //Rezultat proracuna

        public void Racunaj()
        {
            // nalazimo najvecu brzinu vozila za maksimalne obrtaje
            // necemo raditi proracun za brzine vece od toga, jer bi se motor raspao.

            // nalazimo minimalan stepen prenosa (najvecu brzinu menjaca)
            StepenPrenosaMenjaca? poslednjaBrzinaMenjaca = UlazniPodaci.StepeniPrenosaMenjaca.MinBy(x => x.PrenosniOdnos);

            MomentMotora momentNaMaxObrtajima = UlazniPodaci.MomentiMotora.Last();
            int maxBrojObrtaja = momentNaMaxObrtajima.BrojObrtaja;

            decimal ukupniPrenos = poslednjaBrzinaMenjaca!.PrenosniOdnos * UlazniPodaci.GlavniPrenos;

            decimal ugaonaBrzinaTocka = (maxBrojObrtaja * 3.14M / 30) / ukupniPrenos;

            decimal periferijskaBrzinaTocka = ugaonaBrzinaTocka * UlazniPodaci.PoluprecnikTocka;

            decimal periferijskaBrzinaTockaUKmNaSat = (periferijskaBrzinaTocka / 1000) * 3.6M;

            // do ove brzine vozila radimo proracun.
            decimal maksimalnaRacunskaBrzina = Math.Floor((periferijskaBrzinaTockaUKmNaSat / 10)) * 10;

            for (int v = 10; v <= maksimalnaRacunskaBrzina; v += 10)
            {
                MomentiNaTockovimaZaBrzinuVozila moment = new MomentiNaTockovimaZaBrzinuVozila();
                moment.BrzinaVozila = v;
                foreach (StepenPrenosaMenjaca spm in UlazniPodaci.StepeniPrenosaMenjaca)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.BrzinaVozila = moment.BrzinaVozila;
                    dataPoint.PrenosniOdnosMenjaca = spm.PrenosniOdnos;
                    dataPoint.RedniBrojStepenaPrenosaMenjaca = spm.RedniBrojStepenaPrenosa;

                    dataPoint.IzracunajBrojObrtajaMotora(
                        brzina: moment.BrzinaVozila,
                        glavniPrenos: UlazniPodaci.GlavniPrenos,
                       poluprecnikTocka: UlazniPodaci.PoluprecnikTocka,
                       stepeniPrenosaMenjaca: UlazniPodaci.StepeniPrenosaMenjaca);

                    dataPoint.IzracunajMomentMotoraPriObrtajima(
                        momentiMotora: UlazniPodaci.MomentiMotora);

                    dataPoint.IzaracunajMomentNaTockovima(
                        glavniPrenos: UlazniPodaci.GlavniPrenos);

                    moment.DataPoints.Add(
                        dataPoint.RedniBrojStepenaPrenosaMenjaca,
                        dataPoint);
                }

                MomentiNaTockovimaZaSveBrzineVozila.Add(
                    moment.BrzinaVozila,
                    moment);
            }
        }
    }
}