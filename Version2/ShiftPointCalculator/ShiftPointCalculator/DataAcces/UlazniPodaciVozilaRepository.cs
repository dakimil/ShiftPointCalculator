using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class UlazniPodaciVozilaRepository
    {
        public static void Save(UlazniPodaci up)
        {
            // brisemo sve za vozilo
            VoziloDataProvider.Delete(
                up.NazivVozila);

            // dodajemo jedno po jedno
            int voziloId = VoziloDataProvider.Insert(
                up.NazivVozila);

            PoluprecnikTockaDataProvider.Insert(
                voziloId: voziloId,
                poluprecnik: up.PoluprecnikTocka);

            GlavniPrenosDataProvider.Insert(
                voziloId: voziloId,
                prenosniOdnos: up.GlavniPrenos);

            // INSERT u petlji

            foreach (StepenPrenosaMenjaca stepenPrenosaMenjaca in up.StepeniPrenosaMenjaca)
            {
                StepenPrenosaMenjacaDataProvider.Insert(
                    voziloId: voziloId,
                    redniBroj: stepenPrenosaMenjaca.RedniBrojStepenaPrenosa,
                    prenosniOdnos: stepenPrenosaMenjaca.PrenosniOdnos);
            }

            foreach(MomentMotora momentMotora in up.MomentiMotora)
            {
                MomentMotoraDataProvider.Insert(
                voziloId: voziloId,
                brojObrtaja: momentMotora.BrojObrtaja,
                momentPriObrtajima: momentMotora.MomentPriObrtajima
                );
            }
        }

        public static UlazniPodaci GetByVoziloId(int VoziloId)
        {
            UlazniPodaci up = new UlazniPodaci();

            up.NazivVozila = VoziloDataProvider.GetById(VoziloId).NazivVozila;

            up.PoluprecnikTocka = PoluprecnikTockaDataProvider.GetByVoziloId(VoziloId);

            up.MomentiMotora = MomentMotoraDataProvider.GetByVoziloId(VoziloId);

            up.GlavniPrenos = GlavniPrenosDataProvider.GetByVoziloId(VoziloId);

            up.StepeniPrenosaMenjaca = StepenPrenosaMenjacaDataProvider.GetByVoziloId(VoziloId);

            return up;
        }
    }
}
