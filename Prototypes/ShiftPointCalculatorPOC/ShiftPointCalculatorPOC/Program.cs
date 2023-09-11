using System;
using System.Text;

namespace ShiftPointCalculatorPOC
{
    internal class Program
    {
        private const string DATA_DELIMITER = "-------------";

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            String fileFullPath = @"VehicleData\Primer ulaznog fajla.txt";

            List<string> fileLines = new List<string>();

            // index of data in file
            int dataOrdinalNumber = 0;

            VehicleInputData vehicleInputData = new VehicleInputData();

            IEnumerable<string> lines = File.ReadLines(fileFullPath, Encoding.UTF8);
                        
            foreach (string line in lines)
            {
                // data is terminated                 
                if (line.Equals(DATA_DELIMITER))
                {
                    // data delimiter reached
                    dataOrdinalNumber++;

                    continue;
                }

                switch (dataOrdinalNumber)
                {
                    case 0:
                        // wheel radius
                        vehicleInputData.WheelRadius = Convert.ToDecimal(line);
                        break;
                    case 1:
                        vehicleInputData.FinalDriveRatio = Convert.ToDecimal(line);
                        break;
                    case 2:
                        vehicleInputData.AddGearboxRatio(
                            line: line);
                        break;

                }
            }

            //definisanje r_tocka,stepen_dif
            decimal wheelRadius = Convert.ToDecimal(fileLines[0]);

            decimal finalDriveRatio = Convert.ToDecimal(fileLines[1]);



        }
    }
}