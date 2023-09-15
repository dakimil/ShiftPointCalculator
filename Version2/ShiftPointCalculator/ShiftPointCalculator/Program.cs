﻿using System.Text;

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
                    up.PoluprecnikTocka = Convert.ToInt32(linija);
                }
                else if (br_separatora == 1)
                {
                    up.PrenosniOdnosUDiferncijalu = Convert.ToInt32(linija);
                }
                else if (br_separatora == 2)
                {
                    StepenPrenosaMenjaca spm = new StepenPrenosaMenjaca();
                    spm.RedniBrojStepenaPrenosa = stepen;
                    spm.PrenosniOdnos = Convert.ToInt32(linija);
                    up.StepeniPrenosaMenjaca.Add(spm);
                    stepen++;
                }
                else
                {
                    MomentMotora mm = new MomentMotora();
                    string[] pom = linija.Split('\t');
                    mm.BrojObrtaja = Convert.ToInt32(pom[0]);
                    mm.MomentPriObrtajima = Convert.ToInt32(pom[1]);
                    up.MomentiMotora.Add(mm);
                }
            }

            return up;
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