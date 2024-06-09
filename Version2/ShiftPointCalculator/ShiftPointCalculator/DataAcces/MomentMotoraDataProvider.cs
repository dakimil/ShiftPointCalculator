using Microsoft.Data.SqlClient;
using ShiftPointCalculator.QueryResults;
using ShiftPointCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class MomentMotoraDataProvider
    {
        private const string INSERT = @"
        INSERT INTO dbo.MomentMotora
(
	VoziloId
	,BrojObrtaja
	,MomentPriObrtajima
)
OUTPUT inserted.Id
VALUES
(
	@VoziloId
	,@BrojObrtaja
	,@MomentPriObrtajima
);
        ";

        private const string SELECT_BY_VOZILO_ID = @"
SELECT
	Id
	,VoziloId
	,BrojObrtaja
	,MomentPriObrtajima
FROM
	dbo.MomentMotora 
WHERE
	VoziloId = @VoziloId;
"
;

        public static int Insert(int voziloId, int brojObrtaja, decimal momentPriObrtajima)
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

            SqlParameter prmBrojObrtaja = new SqlParameter();
            prmBrojObrtaja.ParameterName = "BrojObrtaja";
            prmBrojObrtaja.SqlDbType = System.Data.SqlDbType.Int;
            prmBrojObrtaja.Value = brojObrtaja;
            cmd.Parameters.Add(prmBrojObrtaja);

            SqlParameter prmPrenosniOdnos = new SqlParameter();
            prmPrenosniOdnos.ParameterName = "MomentPriObrtajima";
            prmPrenosniOdnos.SqlDbType = System.Data.SqlDbType.Decimal;
            prmPrenosniOdnos.Precision = 4;
            prmPrenosniOdnos.Scale = 1;
            prmPrenosniOdnos.Value = momentPriObrtajima;
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

        public static List<MomentMotora> GetByVoziloId(int voziloId)
        {
            List<MomentMotora> lista = new List<MomentMotora>();

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
                MomentMotora mm = new MomentMotora();
                mm.BrojObrtaja = Convert.ToInt32(reader["BrojObrtaja"]);
                mm.MomentPriObrtajima = Convert.ToDecimal(reader["MomentPriObrtajima"]);

                lista.Add(mm);
            }

            cn.Close();

            return lista;
        }
    }
}
