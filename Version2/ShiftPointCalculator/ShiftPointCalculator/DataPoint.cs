﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator
{
    public class DataPoint
    {
        public decimal MomentNaTocku { get; set; }
        public decimal MomentMotoraPriObrtajima { get; set; }
        public decimal BrojObrtajaMotora { get; set; }
        public decimal PrenosniOdnosMenjaca { get; set; }
        public int RedniBrojStepenaPrenosaMenjaca { get; set; }
        public decimal BrzinaVozila { get; set; }
        public void IzracunajBrojObrtajaMotora(
            decimal brzina, 
            decimal glavniPrenos, 
            decimal poluprecnikTocka, 
            List<StepenPrenosaMenjaca> stepeniPrenosaMenjaca)
        {
            this.BrojObrtajaMotora = brzina * (1000.00m / 3600.00m) * glavniPrenos / (poluprecnikTocka / 1000.00m)
                * stepeniPrenosaMenjaca[RedniBrojStepenaPrenosaMenjaca - 1].PrenosniOdnos * 60.00m / (2.00m * 3.1415m);
        }
        public void IzracunajMomentMotoraPriObrtajima(List<MomentMotora> momentiMotora)
        {
            decimal donjiObrtajiZaokrNa100 = Math.Floor(this.BrojObrtajaMotora / 100m);
            Nullable<Decimal> donjiMoment = null, gornjiMoment = null;
            for(int i = 0; i<momentiMotora.Count; i++)
            {
                if (momentiMotora[i].BrojObrtaja == donjiObrtajiZaokrNa100 * 100m)
                {
                    donjiMoment = momentiMotora[i].MomentPriObrtajima;
                    gornjiMoment= momentiMotora[i+1].MomentPriObrtajima;
                    break;
                }
            }
            if (gornjiMoment != null
            && donjiMoment != null)
            {
                this.MomentMotoraPriObrtajima = donjiMoment.Value + ((this.BrojObrtajaMotora - (donjiObrtajiZaokrNa100 * 100m)) * 
                    ((gornjiMoment.Value - donjiMoment.Value) / 100m));
            }
        }
        public void IzaracunajMomentNaTockovima(decimal glavniPrenos)
        {
            this.MomentNaTocku = this.MomentMotoraPriObrtajima * this.PrenosniOdnosMenjaca * glavniPrenos;
        }

    }
}
