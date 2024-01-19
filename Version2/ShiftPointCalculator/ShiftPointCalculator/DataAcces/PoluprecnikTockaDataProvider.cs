using Microsoft.Data.SqlClient;
using ShiftPointCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class PoluprecnikTockaDataProvider
    {
        private const string INSERT = @"
INSERT INTO dbo.PoluprecnikTocka
(
	VoziloId
	,Poluprecnik
)
OUTPUT inserted.Id
VALUES
(
	@VoziloId
	,@Poluprecnik
);

";

        private const string SELECT_BY_VOZILO_ID = @"
SELECT
	Id
	,VoziloId
	,Poluprecnik
FROM
	dbo.PoluprecnikTocka
WHERE
	VoziloId = @VoziloId;
";
        public static int Insert(int voziloId, int poluprecnik)
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


            SqlParameter prmPoluprecnik = new SqlParameter();
            prmPoluprecnik.ParameterName = "Poluprecnik";
            prmPoluprecnik.SqlDbType = System.Data.SqlDbType.Int;
            prmPoluprecnik.Value = poluprecnik;
            cmd.Parameters.Add(prmPoluprecnik);

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

        public static int GetByVoziloId(int voziloId)
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

            int poluprecnik = Convert.ToInt32(reader["poluprecnik"]);

            cn.Close();

            return poluprecnik;
        }
    }
}
