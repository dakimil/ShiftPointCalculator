﻿using Microsoft.Data.SqlClient;
using ShiftPointCalculator.QueryResults;
using ShiftPointCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.DataAcces
{
    public static class VoziloDataProvider
    {
        private const string DELETE = @"
            --redosled je bitan zbog FK
            
DECLARE @VoziloId INT;

SELECT @VoziloId = id
FROM dbo.Vozilo
WHERE NazivVozila = @NazivVozila;

IF(@VoziloId IS NULL)
BEGIN
	--NEMA VOZILA SA TIM NAZIVOM
	RETURN;
END

DELETE dbo.MomentMotora
WHERE VoziloId = @VoziloId

DELETE dbo.GlavniPrenos
WHERE VoziloId = @VoziloId

DELETE dbo.StepenPrenosaMenjaca
WHERE VoziloId = @VoziloId

DELETE dbo.PoluprecnikTocka
WHERE VoziloId = @VoziloId

DELETE dbo.Vozilo
WHERE Id = @VoziloId;
        ";

        private const string INSERT = @"
INSERT INTO dbo.Vozilo(
	NazivVozila
) 
OUTPUT inserted.Id, inserted.NazivVozila
VALUES(
 @NazivVozila
);
";

        private const string SELECT_ALL = @"
SELECT Id, NazivVozila 
FROM dbo.Vozilo;
";

        private const string SELECT_BY_ID = @"
SELECT
	Id
	,NazivVozila
FROM
	dbo.Vozilo
WHERE
    Id = @Id;

";

        public static void Delete(string nazivVozila)
        {
            SqlConnection cn = new SqlConnection(ConnectionStrings.ConnectionString);

            try
            {
                cn.Open();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            //pitaj tatu da ti pokaze Sql profiler
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = DELETE;
            SqlParameter prmNazivVozila = new SqlParameter();
            prmNazivVozila.ParameterName = "@NazivVozila";
            prmNazivVozila.Size = 50;
            prmNazivVozila.SqlDbType = System.Data.SqlDbType.NVarChar;
            prmNazivVozila.Value = nazivVozila;

            cmd.Parameters.Add(prmNazivVozila);

            try
            {
                cmd.ExecuteNonQuery();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            cn.Close();

        }

        public static int Insert(string nazivVozila)
        {
            int? id = null;

            SqlConnection cn = new SqlConnection(ConnectionStrings.ConnectionString);

            try
            {
                cn.Open();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = INSERT;

            SqlParameter prmNazivVozila = new SqlParameter();
            prmNazivVozila.ParameterName = "NazivVozila"; ;
            prmNazivVozila.Size = 50;
            prmNazivVozila.SqlDbType = System.Data.SqlDbType.NVarChar;
            prmNazivVozila.Value = nazivVozila;

            cmd.Parameters.Add(prmNazivVozila);

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

        public static List<VoziloQueryResult> GetAll()
        {
            List<VoziloQueryResult> listaVozila = new List<VoziloQueryResult>();

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
            cmd.CommandText = SELECT_ALL;
            SqlDataReader reader = cmd.ExecuteReader();

            //citamo dok ima redova
            while (reader.Read())
            {
                VoziloQueryResult voziloQueryResult = new VoziloQueryResult();
                voziloQueryResult.NazivVozila = Convert.ToString(reader["NazivVozila"]);
                voziloQueryResult.Id = Convert.ToInt32(reader["Id"]);
                listaVozila.Add(voziloQueryResult);
            }

            cn.Close();

            return listaVozila;
        }

        public static VoziloQueryResult GetById(int id)
        {
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
            cmd.CommandText = SELECT_BY_ID;
            SqlParameter prmVoziloId = new SqlParameter();
            prmVoziloId.ParameterName = "id";
            prmVoziloId.SqlDbType = System.Data.SqlDbType.Int;
            prmVoziloId.Value = id;

            cmd.Parameters.Add(prmVoziloId);

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            VoziloQueryResult voziloQueryResult = new VoziloQueryResult();
            voziloQueryResult.NazivVozila = Convert.ToString(reader["NazivVozila"]);
            voziloQueryResult.Id = Convert.ToInt32(reader["Id"]);


            cn.Close();

            return voziloQueryResult;
        }
    }  
}
