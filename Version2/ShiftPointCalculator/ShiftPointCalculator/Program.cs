using ShiftPointCalculator.DataAcces;
using ShiftPointCalculator.Repositories;
using System.Globalization;
using System.Text;

namespace ShiftPointCalculator
{
    internal class Program
    {
        // test commit
        static UlazniPodaci Parsiranje(
            IEnumerable<string> linije)
        {
            const string separator = "-------------";
            UlazniPodaci up = new UlazniPodaci();
            int br_separatora = 0, stepen = 1;
            foreach (string linija in linije)
            {
                if (linija.Equals(separator))
                {
                    br_separatora++;
                    continue;
                }

                if (br_separatora == 0)
                {
                    up.NazivVozila = linija;
                }
                else if (br_separatora == 1)
                {
                    up.PoluprecnikTocka = Convert.ToInt32(linija);
                }
                else if (br_separatora == 2)
                {
                    up.GlavniPrenos = Convert.ToDecimal(linija);
                }
                else if (br_separatora == 3)
                {
                    StepenPrenosaMenjaca spm = new StepenPrenosaMenjaca();
                    spm.RedniBrojStepenaPrenosa = stepen;
                    spm.PrenosniOdnos = Convert.ToDecimal(linija);
                    up.StepeniPrenosaMenjaca.Add(spm);
                    stepen++;
                }
                else
                {
                    MomentMotora mm = new MomentMotora();
                    string[] pom = linija.Split('\t');
                    mm.BrojObrtaja = Convert.ToInt32(pom[0]);
                    mm.MomentPriObrtajima = Convert.ToDecimal(pom[1]);
                    up.MomentiMotora.Add(mm);
                }
            }

            return up;
        }

        static void Main(string[] args)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            // diskutuj "." i ","
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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

            Racunaj(kola);
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
    }
}