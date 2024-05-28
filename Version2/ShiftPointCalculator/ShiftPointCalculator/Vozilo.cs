using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ovde racunamo momente na tockovima za sve brzine vozila
namespace ShiftPointCalculator
{
    public class Vozilo
    {
        public Vozilo(int id, UlazniPodaci up)
        {
            this.Id = id;
            this.UlazniPodaci = up;
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
            for (int v = 10; v <= 230; v += 10)
            {
                MomentiNaTockovimaZaBrzinuVozila moment = new MomentiNaTockovimaZaBrzinuVozila();
                moment.BrzinaVozila = v;
                foreach (StepenPrenosaMenjaca spm in this.UlazniPodaci.StepeniPrenosaMenjaca)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.BrzinaVozila = moment.BrzinaVozila;
                    dataPoint.PrenosniOdnosMenjaca = spm.PrenosniOdnos;
                    dataPoint.RedniBrojStepenaPrenosaMenjaca = spm.RedniBrojStepenaPrenosa;

                    dataPoint.IzracunajBrojObrtajaMotora(
                        brzina: moment.BrzinaVozila,
                        glavniPrenos: this.UlazniPodaci.GlavniPrenos,
                       poluprecnikTocka: this.UlazniPodaci.PoluprecnikTocka,
                       stepeniPrenosaMenjaca: this.UlazniPodaci.StepeniPrenosaMenjaca);

                    dataPoint.IzracunajMomentMotoraPriObrtajima(
                        momentiMotora: this.UlazniPodaci.MomentiMotora);

                    dataPoint.IzaracunajMomentNaTockovima(
                        glavniPrenos: this.UlazniPodaci.GlavniPrenos);

                    moment.DataPoints.Add(
                        dataPoint.RedniBrojStepenaPrenosaMenjaca,
                        dataPoint);
                }

                this.MomentiNaTockovimaZaSveBrzineVozila.Add(
                    moment.BrzinaVozila,
                    moment);
            }
        }
    }
}