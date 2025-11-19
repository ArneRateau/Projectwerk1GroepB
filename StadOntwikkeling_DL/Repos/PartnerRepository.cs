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
        private string connectionString;

        public PartnerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //nog niet klaar
        public List<Partner> GetPartners()
        {
            List<Partner> partners = new List<Partner>();
            string queryPartner = "select p.PartnerId, p.Naam, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.Huisnummer from Partner p join Locatie l on p.LocatieId = l.Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                            Partner partner = new Partner((int)reader["PartnerId"], (string)reader["Naam"], locatie, (string)reader["Email"],new List<ProjectPartner>());
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
        //nog niet klaar
        public int /*void*/ MakeNewPartner(Partner partner/*, Locatie locatie*/)
        {
            int locatieId;
            int partnerId;
            string queryLocatie = "insert into Locatie(Straat,Gemeente,Postcode,Wijk,Huisnummer) output inserted.LocatieId values(@Straat,@Gemeente,@Postcode,@Wijk,@Huisnummer)";
            string query = "insert into Partner(Naam,locatieId,Email) output inserted.partnerId values(@Naam,@LocatieId,@Email)";
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            using(SqlCommand cmdAddLocatie = connection.CreateCommand())
            using (SqlCommand cmdAddPartner = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                cmdAddLocatie.Transaction = transaction;
                cmdAddPartner.Transaction = transaction;
                cmdAddLocatie.CommandText = query;
                cmdAddPartner.CommandText = query;
                try
                {
                    cmdAddLocatie.Parameters.AddWithValue("@Straat", partner.Locatie.Straat);
                    cmdAddLocatie.Parameters.AddWithValue("@Gemeente",partner.Locatie.Gemeente);
                    cmdAddLocatie.Parameters.AddWithValue("@Postcode",partner.Locatie.Postcode);
                    cmdAddLocatie.Parameters.AddWithValue("@Wijk", partner.Locatie.Wijk);
                    cmdAddLocatie.Parameters.AddWithValue("@Huisnummer", partner.Locatie.Huisnummer);
                    locatieId = (int)cmdAddLocatie.ExecuteScalar();

                    cmdAddPartner.Parameters.AddWithValue("@Naam", partner.Naam);
                    cmdAddPartner.Parameters.AddWithValue("@LocatieId", partner.Locatie.Id);
                    cmdAddPartner.Parameters.AddWithValue("@Email", partner.Email);
                    partnerId = (int)cmdAddPartner.ExecuteScalar();

                    transaction.Commit();
                    return partnerId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("", ex);
                }
            }
        }
    }
}
