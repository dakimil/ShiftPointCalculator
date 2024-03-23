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
                    izabranaKomanda = GlavniMeni();

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
            /*
            IEnumerable<string> linije = default!;
            try
            {
                String fullName = @"VehicleData\Primer ulaznog fajla.txt";
                linije = File.ReadLines(fullName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }


            UlazniPodaci ulazniPodaci = null;
            try
            {
                ulazniPodaci = Parsiranje(linije);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }

            UlazniPodaciVozilaRepository.Save(ulazniPodaci);

            Vozilo kola = new Vozilo();

            List<QueryResults.VoziloQueryResult> listaVozila = VoziloDataProvider.GetAll();

            QueryResults.VoziloQueryResult prvoVozilo = listaVozila.First();
            kola.UlazniPodaci = UlazniPodaciVozilaRepository.GetByVoziloId(prvoVozilo.Id);

            Racunaj(kola);*/
        }

        private static void Racunaj(Vozilo kola)
        {
            for (int v = 10; v <= 230; v += 10)
            {
                MomentiNaTockovimaZaBrzinuVozila moment = new MomentiNaTockovimaZaBrzinuVozila();
                moment.BrzinaVozila = v;
                foreach (StepenPrenosaMenjaca spm in kola.UlazniPodaci.StepeniPrenosaMenjaca)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.BrzinaVozila = moment.BrzinaVozila;
                    dataPoint.PrenosniOdnosMenjaca = spm.PrenosniOdnos;
                    dataPoint.RedniBrojStepenaPrenosaMenjaca = spm.RedniBrojStepenaPrenosa;

                    dataPoint.IzracunajBrojObrtajaMotora(
                        brzina: moment.BrzinaVozila,
                        glavniPrenos: kola.UlazniPodaci.GlavniPrenos,
                       poluprecnikTocka: kola.UlazniPodaci.PoluprecnikTocka,
                       stepeniPrenosaMenjaca: kola.UlazniPodaci.StepeniPrenosaMenjaca);

                    dataPoint.IzracunajMomentMotoraPriObrtajima(
                        momentiMotora: kola.UlazniPodaci.MomentiMotora);

                    dataPoint.IzaracunajMomentNaTockovima(
                        glavniPrenos: kola.UlazniPodaci.GlavniPrenos);

                    moment.DataPoints.Add(
                        dataPoint.RedniBrojStepenaPrenosaMenjaca,
                        dataPoint);
                }

                kola.MomentiNaTockovimaZaSveBrzineVozila.Add(
                    moment.BrzinaVozila,
                    moment);
            }
        }

        private static Komande? GlavniMeni()
        {
            IEnumerable<Komande> commands = Enum.GetValues<Komande>();

            Console.WriteLine("Izaberi:");

            foreach (var command in commands)
            {
                Console.WriteLine($"{(int)command} - {command}");
            }

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
    }
}