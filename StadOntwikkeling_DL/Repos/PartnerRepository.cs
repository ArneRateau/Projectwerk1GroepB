using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_DL.Repos
{
    public class PartnerRepository : IPartnerRepository
    {
        private string _connectionString;

        public PartnerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Partner> GetAllPartners()
        {
            List<Partner> partners = new List<Partner>();

            string sql = @"
        SELECT PartnerId, Naam, Email
        FROM Partner;
    ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partners.Add(new Partner(
                            id: reader.GetInt32(0),
                            naam: reader.GetString(1),
                            email: reader.GetString(2)
                        ));
                    }
                }
            }

            return partners;
        }


        public List<Partner> GetPartners()
        {
            List<Partner> partners = new List<Partner>();
            string queryPartner = "select p.PartnerId, p.Naam, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.Huisnummer from Partner p join Locatie l on p.LocatieId = l.Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = queryPartner;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Locatie locatie = new Locatie((int)reader["LocatieId"], (string)reader["Straat"], (string)reader["Postcode"], (string)reader["Gemeente"], (string)reader["Wijk"],(string)reader["Huisnummer"]);
                            Partner partner = new Partner((int)reader["PartnerId"], (string)reader["Naam"], (string)reader["Email"]);
                            partners.Add(partner);
                        }
                        return partners;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Geef partners", ex);
                }
            }
        }
        public List<Partner> GetPartnersByProjectId(int projectId)
        {
            List<Partner> partners = new List<Partner>();

            string sql = @"
        SELECT pa.PartnerId, pa.Naam, pa.Email
        FROM Partner pa
        JOIN ProjectPartner pp ON pa.PartnerId = pp.ProjectPartnerId
        WHERE pp.ProjectId = @projectId;
    ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@projectId", projectId);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partners.Add(new Partner(
                            id: reader.GetInt32(0),
                            naam: reader.GetString(1),
                            email: reader.GetString(2)
                        ));
                    }
                }
            }

            return partners;
        }

        public int MaakPartner(string naam, string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            INSERT INTO Partner (Naam, Email)
            VALUES (@Naam, @Email);

            SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Naam", naam);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int newId = (int)cmd.ExecuteScalar();
                    return newId;
                }
            }
        }


        //nog niet klaar
        //public int /*void*/ MakeNewPartner(Partner partner/*, Locatie locatie*/)
        //{
        //    int locatieId;
        //    int partnerId;
        //    string queryLocatie = "insert into Locatie(Straat,Gemeente,Postcode,Wijk,Huisnummer) output inserted.LocatieId values(@Straat,@Gemeente,@Postcode,@Wijk,@Huisnummer)";
        //    string query = "insert into Partner(Naam,locatieId,Email) output inserted.partnerId values(@Naam,@LocatieId,@Email)";
        //    using (SqlConnection connection = new SqlConnection(_connectionString)) 
        //    using(SqlCommand cmdAddLocatie = connection.CreateCommand())
        //    using (SqlCommand cmdAddPartner = connection.CreateCommand())
        //    {
        //        connection.Open();
        //        SqlTransaction transaction = connection.BeginTransaction();
        //        cmdAddLocatie.Transaction = transaction;
        //        cmdAddPartner.Transaction = transaction;
        //        cmdAddLocatie.CommandText = query;
        //        cmdAddPartner.CommandText = query;
        //        try
        //        {
        //            cmdAddLocatie.Parameters.AddWithValue("@Straat", partner.Locatie.Straat);
        //            cmdAddLocatie.Parameters.AddWithValue("@Gemeente",partner.Locatie.Gemeente);
        //            cmdAddLocatie.Parameters.AddWithValue("@Postcode",partner.Locatie.Postcode);
        //            cmdAddLocatie.Parameters.AddWithValue("@Wijk", partner.Locatie.Wijk);
        //            cmdAddLocatie.Parameters.AddWithValue("@Huisnummer", partner.Locatie.Huisnummer);
        //            locatieId = (int)cmdAddLocatie.ExecuteScalar();

        //            cmdAddPartner.Parameters.AddWithValue("@Naam", partner.Naam);
        //            cmdAddPartner.Parameters.AddWithValue("@LocatieId", partner.Locatie.Id);
        //            cmdAddPartner.Parameters.AddWithValue("@Email", partner.Email);
        //            partnerId = (int)cmdAddPartner.ExecuteScalar();

        //            transaction.Commit();
        //            return partnerId;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw new Exception("", ex);
        //        }
        //    }
        //}
    }
}
