using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator
{
    public class MomentiNaTockovimaZaBrzinuVozila
    {
        public int BrzinaVozila { get; set; }
        /// <summary>
        /// Kljuc je redni broj stepena prenosa menjaca 
        /// </summary>
        public Dictionary<int, DataPoint> DataPoints { get;} = new Dictionary<int, DataPoint>();
        
    }
}
