using Microsoft.Data.SqlClient;
using ShiftPointCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class GlavniPrenosDataProvider
    {
        private const string INSERT = @"
INSERT INTO dbo.GlavniPrenos
(
	VoziloId
	,PrenosniOdnos
)
OUTPUT inserted.Id
VALUES
(
	@VoziloId
	,@PrenosniOdnos
);

";

        private const string SELECT_BY_VOZILO_ID = @"
SELECT
	Id
	,VoziloId
	,prenosniOdnos
FROM
	dbo.GlavniPrenos
WHERE
	VoziloId = @VoziloId;

";

        //vraca Id
        public static int Insert(int voziloId, decimal prenosniOdnos)
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

        public static decimal GetByVoziloId(int voziloId)
        {
            SqlConnection cn = new SqlConnection(ConnectionStrings.ConnectionString);

            try
            {
                cn.Open();

                Console.WriteLine($"Connection na {cn.ConnectionString} otvorena");
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

            // citamo jedan red

            reader.Read();

            decimal prenosniOdnos = Convert.ToDecimal(reader["PrenosniOdnos"]);

            cn.Close();

            return prenosniOdnos;
        }
    }
}
