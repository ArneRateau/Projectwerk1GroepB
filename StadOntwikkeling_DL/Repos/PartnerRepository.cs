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
            string queryPartner = "select p.Id,p.naam, l.straat, l.gemeente, l.postcode,l.wijk,l.huisnummer from Partner join Locatie l on p.LocatieId = l.Id";
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
                            //partners.Add(new Partner((int)reader["Id"], (string)reader["Naam"],new Locatie((string)reader["straat"],(string)reader["postcode"],(string)reader["gemeente"],(string)reader["wijk"]/*,huisnummer*/ ), (string)reader["email"],/**/));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Geef partners", ex);
                }
                return partners;
            }
        }
        //nog niet klaar
        public void MakeNewPartner()
        {

        }

    }
}
