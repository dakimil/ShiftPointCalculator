namespace ShiftPointCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //ucitavanje fajla
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string fullName = System.IO.Path.Combine(desktopPath, "Primer ulaznog fajla.txt");
            String fullName = @"VehicleData\Primer ulaznog fajla.txt";
            List<string> linije = new List<string>();
        }
    }
}