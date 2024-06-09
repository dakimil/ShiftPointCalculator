using Microsoft.Data.SqlClient;
using ShiftPointCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class StepenPrenosaMenjacaDataProvider
    {
        private const string INSERT = @"
INSERT INTO dbo.StepenPrenosaMenjaca
(
	VoziloId
	,RedniBroj
	,PrenosniOdnos
)
OUTPUT inserted.Id
VALUES
(
	@VoziloId
	,@RedniBroj
	,@PrenosniOdnos
);
";

        private const string SELECT_BY_VOZILO_ID = @"
SELECT
	Id
	,VoziloId
	,RedniBroj
	,PrenosniOdnos
FROM
	dbo.StepenPrenosaMenjaca
WHERE
	VoziloId = @VoziloId;
";
        public static int Insert(int voziloId, int redniBroj, decimal prenosniOdnos)
        {
            int? id = null;

            SqlConnection cn = new SqlConnection(ConnectionStrings.ConnectionString);

            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = INSERT;

            SqlParameter prmVoziloId = new SqlParameter();
            prmVoziloId.ParameterName = "VoziloId";
            prmVoziloId.SqlDbType = System.Data.SqlDbType.Int;
            prmVoziloId.Value = voziloId;
            cmd.Parameters.Add(prmVoziloId);


            SqlParameter prmRedniBroj = new SqlParameter();
            prmRedniBroj.ParameterName = "Rednibroj";
            prmRedniBroj.SqlDbType = System.Data.SqlDbType.Int;
            prmRedniBroj.Value = redniBroj;
            cmd.Parameters.Add(prmRedniBroj);

            SqlParameter prmPrenosniOdnos = new SqlParameter();
            prmPrenosniOdnos.ParameterName = "PrenosniOdnos";
            prmPrenosniOdnos.SqlDbType = System.Data.SqlDbType.Decimal;
            prmPrenosniOdnos.Precision = 4;
            prmPrenosniOdnos.Scale = 3;
            prmPrenosniOdnos.Value = prenosniOdnos;
            cmd.Parameters.Add(prmPrenosniOdnos);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                id = Convert.ToInt32(reader["Id"]);

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            cn.Close();

            return id.Value;
        }

        public static List<StepenPrenosaMenjaca> GetByVoziloId(int voziloId)
        {
            List<StepenPrenosaMenjaca> lista = new List<StepenPrenosaMenjaca>();

            SqlConnection cn = new SqlConnection(ConnectionStrings.ConnectionString);

            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = SELECT_BY_VOZILO_ID;

            SqlParameter prmVoziloId = new SqlParameter();
            prmVoziloId.ParameterName = "voziloId";
            prmVoziloId.SqlDbType = System.Data.SqlDbType.Int;
            prmVoziloId.Value = voziloId;

            cmd.Parameters.Add(prmVoziloId);

            SqlDataReader reader = cmd.ExecuteReader();

            // citamo dok ima redova
            while (reader.Read())
            {
                StepenPrenosaMenjaca mm = new StepenPrenosaMenjaca();
                mm.RedniBrojStepenaPrenosa = Convert.ToInt32(reader["RedniBroj"]);
                mm.PrenosniOdnos = Convert.ToDecimal(reader["PrenosniOdnos"]);
                lista.Add(mm);
            }

            cn.Close();

            return lista;
        }
    }
}
