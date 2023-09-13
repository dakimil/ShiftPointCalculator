using System.Text;

namespace ShiftPointCalculator
{
    internal class Program
    {
        static UlazniPodaci Parsiranje(IEnumerable<string> linije)
        {
            const string separator = "-------------";
            UlazniPodaci UP = new UlazniPodaci();
            int br = 0, stepen = 1;
            foreach (string linija in linije)
            {
                if (linija.Equals(separator))
                {
                    br++;
                    continue;
                }
                if (br == 0)
                {
                    UP.PoluprecnikTocka = Convert.ToInt32(linija);
                }
                else if (br == 1)
                {
                    UP.PrenosniOdnosUDiferncijalu = Convert.ToInt32(linija);
                }
                else if(br == 2)
                {
                    StepenPrenosaMenjaca SPM = new StepenPrenosaMenjaca();
                    SPM.RedniBrojStepenaPrenosa = stepen;
                    SPM.PrenosniOdnos = Convert.ToInt32(linija);
                    UP.StepeniPrenosaMenjaca.Add(SPM);
                    stepen++;
                }
                else
                {
                    MomentMotora MM = new MomentMotora();
                    string[] pom = linija.Split('\t');
                    MM.BrojObrtaja = Convert.ToInt32(pom[0]);
                    MM.MomentPriObrtajima = Convert.ToInt32(pom[1]);
                    UP.MomentiMotora.Add(MM);
                }
            }

            return UP;
        }
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //ucitavanje fajla
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string fullName = System.IO.Path.Combine(desktopPath, "Primer ulaznog fajla.txt");
            String fullName = @"VehicleData\Primer ulaznog fajla.txt";
            IEnumerable<string> linije = File.ReadLines(fullName, Encoding.UTF8);
            UlazniPodaci Ulaz=new UlazniPodaci();
            Ulaz = Parsiranje(linije);


        }
    }
}