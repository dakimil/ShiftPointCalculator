using System.Text;

namespace ShiftPointCalculator
{
    internal class Program
    {
        static UlazniPodaci Parsiranje(IEnumerable<string> linije)
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
                    up.PoluprecnikTocka = Convert.ToDecimal(linija);
                }
                else if (br_separatora == 1)
                {
                    up.PrenosniOdnosUDiferncijalu = Convert.ToDecimal(linija);
                }
                else if (br_separatora == 2)
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
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


            IEnumerable<string> linije = default!;
            try
            {
                String fullName = @"VehicleData\Primer ulaznog fajla.txt";
                linije = File.ReadLines(fullName, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return;
            }


            UlazniPodaci ulazniPodaci = null!;

            ulazniPodaci = Parsiranje(linije);

            Vozilo kola = new Vozilo();
            for(int v=10; v<=230; v += 10)
            {
                MomentiNaTockovimaZaBrzinuVozila moment = new MomentiNaTockovimaZaBrzinuVozila();
                moment.BrzinaVozila = v;
                foreach(StepenPrenosaMenjaca spm in ulazniPodaci.StepeniPrenosaMenjaca)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.BrzinaVozila = moment.BrzinaVozila;
                    dataPoint.PrenosniOdnosMenjaca = spm.PrenosniOdnos;
                    dataPoint.RedniBrojStepenaPrenosaMenjaca = spm.RedniBrojStepenaPrenosa;
                    moment.DataPoints.Add(dataPoint.RedniBrojStepenaPrenosaMenjaca, dataPoint);
                }
                kola.MomentiNaTockovimaZaSveBrzineVozila.Add(moment.BrzinaVozila, moment);
            }

        }
    }
}