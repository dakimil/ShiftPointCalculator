using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.BusinessLogic
{
    public class MomentiNaTockovimaZaBrzinuVozila
    {
        public int BrzinaVozila { get; set; }
        /// <summary>
        /// Kljuc je redni broj stepena prenosa menjaca 
        /// </summary>
        public Dictionary<int, DataPoint> DataPoints { get; } = new Dictionary<int, DataPoint>();

        public DataPoint? DataPointWithMaxMoment
        {
            get
            {
                DataPoint? maxDataPoint = DataPoints.Values.MaxBy(x => x.MomentNaTocku);
                return maxDataPoint;
            }
        }
    }
}
