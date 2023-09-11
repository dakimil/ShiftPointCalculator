using System.Net.Http.Headers;
using System.Text;
using System.Xml.Xsl;

List<decimal> obrtaj(decimal brz, decimal r_tocka, decimal stepen_dif, List<decimal> menjac)
{
    List<decimal> obr = new List<decimal>();
    decimal ug_v = brz * (1000.00m / 3600.00m) * stepen_dif / (r_tocka / 1000.00m);
    foreach (decimal c in menjac)
    {
        obr.Add(ug_v * c * 60.00m / (2.00m * 3.1415m));
    }
    return obr;
}

List<decimal?> momenti_motora_za_brzinu_sracunaj(List<decimal> obrtaji_pri_brzini_lista, List<Tuple<decimal, decimal>> momenti)
{
    List<decimal?> momenti_pri_brzini = new List<decimal?>();

    foreach (decimal obrtaji_pri_brzini in obrtaji_pri_brzini_lista)
    {
        decimal donji_obrtaji_zaokr_na_100;

        Nullable<Decimal> donji_moment = null, gornji_moment = null;

        int i = 0;

        donji_obrtaji_zaokr_na_100 = Math.Floor(obrtaji_pri_brzini / 100);

        foreach (Tuple<decimal, decimal> t in momenti)
        {
            if (t.Item1 == donji_obrtaji_zaokr_na_100 * 100.00m)
            {
                donji_moment = t.Item2;
                if ((i + 1) < momenti.Count)
                {
                    gornji_moment = momenti[i + 1].Item2;
                }

                break;
            }
            i++;
        }

        if (gornji_moment != null
            && donji_moment != null)
        {
            decimal moment = donji_moment.Value + ((obrtaji_pri_brzini - (donji_obrtaji_zaokr_na_100 * 100m)) * ((gornji_moment.Value - donji_moment.Value) / 100m));
            momenti_pri_brzini.Add(moment);
        }
        else
        {
            // motor ne radi na ovim obrtajima
            momenti_pri_brzini.Add(null);
        }



    }

    return momenti_pri_brzini;
}


//ucitavanje fajla
//string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//string fullName = System.IO.Path.Combine(desktopPath, "Primer ulaznog fajla.txt");
String fullName = @"VehicleData\Primer ulaznog fajla.txt";
List<string> linije = new List<string>();
//izbacivanje '------'
foreach (string line in File.ReadLines(fullName, Encoding.UTF8))
{
    if (!line.Equals("-------------"))
    {
        linije.Add(line);
    }
}
//definisanje r_tocka,stepen_dif
decimal r_tocka, stepen_dif;

r_tocka = Convert.ToDecimal(linije[0]);

stepen_dif = Convert.ToDecimal(linije[1]);

//menjac
List<decimal> prenosni_odnosi = new List<decimal>();

for (int i = 2; i < 8; i++)
{
    prenosni_odnosi.Add(Convert.ToDecimal(linije[i]));
}
// lista br. obrtaja i njihovih momenata
List<Tuple<decimal, decimal>> momenti = new List<Tuple<decimal, decimal>>();

String[] pom = new string[2];

for (int i = 8; i < linije.Count; i++)
{
    pom = linije[i].Split('\t');
    Tuple<decimal, decimal> t = Tuple.Create(Convert.ToDecimal(pom[0]), Convert.ToDecimal(pom[1]));
    momenti.Add(t);
}

/*
List<decimal> a = obrtaj(
    brz: 100,
    r_tocka: r_tocka,
    stepen_dif: stepen_dif,
    menjac: prenosni_odnosi);
*/

Console.WriteLine(r_tocka);
Console.WriteLine(stepen_dif);
foreach (decimal? i in prenosni_odnosi)
{
    Console.WriteLine(i);
}
foreach (Tuple<decimal, decimal> i in momenti)
{
    Console.WriteLine(i);
}


for (int brzina = 10; brzina <= 230; brzina += 10)
{
    List<decimal> trenutni_obrtaji_za_brzinu = obrtaj(brzina * 1.00m, r_tocka, stepen_dif, prenosni_odnosi);

    List<decimal?> momenti_motora_za_brzinu_lista = momenti_motora_za_brzinu_sracunaj(trenutni_obrtaji_za_brzinu, momenti);

    List<decimal?> momenti_na_tocku_za_brzinu_lista = new List<decimal?>();

    for (int stepen_prenosa = 0; stepen_prenosa < prenosni_odnosi.Count; stepen_prenosa++)
    {
        decimal? moment_motora_za_brzinu = momenti_motora_za_brzinu_lista[stepen_prenosa];

        decimal prenosni_odnos_za_stepen_prenosa = prenosni_odnosi[stepen_prenosa];

        decimal? moment_na_tocku = null;

        if (moment_motora_za_brzinu != null)
        {
            moment_na_tocku = moment_motora_za_brzinu.Value * prenosni_odnos_za_stepen_prenosa * stepen_dif;
        }

        momenti_na_tocku_za_brzinu_lista.Add(moment_na_tocku);
    }

    Console.WriteLine(brzina);

    foreach (decimal? moment_na_tocku_za_brzinu in momenti_na_tocku_za_brzinu_lista)
    {
        if (moment_na_tocku_za_brzinu.HasValue)
        {
            var mZaokruzeno = Math.Round(moment_na_tocku_za_brzinu.Value, 2);

            Console.Write(mZaokruzeno + " ");
        }
        else
        {
            Console.Write("N/A ");
        }
    }

    Console.WriteLine();
}


