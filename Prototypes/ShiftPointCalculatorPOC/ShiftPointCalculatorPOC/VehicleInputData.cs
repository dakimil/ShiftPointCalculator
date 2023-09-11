using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculatorPOC
{
    internal class VehicleInputData
    {
        /// <summary>
        /// WheelRadius in mm
        /// </summary>
        public decimal WheelRadius { get; set; }

        /// <summary>
        /// Final Drive Ratio (gearing of differential).
        /// </summary>
        public decimal FinalDriveRatio { get; set; }

        private List<GearboxRatio> GearboxRatiosPrivate { get; } = new List<GearboxRatio>();

        public IEnumerable<GearboxRatio> GearboxRatios
        {
            get
            {
                return GearboxRatiosPrivate;
            }
        }

        public void AddGearboxRatio(
            string line)
        {
            GearboxRatio gearboxRatio = new GearboxRatio(
                gearNumber: GearboxRatiosPrivate.Count + 1,
                gearRatio: Convert.ToDecimal(line));

            GearboxRatiosPrivate.Add(gearboxRatio);
        }
    }
}
