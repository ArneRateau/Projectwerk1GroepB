using Microsoft.Data.SqlClient;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_DL.Repos
{
    public class ProjectPartnerRepository : IProjectPartnerRepository
    {
        private string connectionString;

        public ProjectPartnerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        // kan gebruikt worden om te kijken of een partner toestemming heeft om een project aan te maken
        public bool HeeftPartnerRolVoorProject(Project pr, Partner pa, string rol)
        {
            string query = "SElECT 1 FROM ProjectPartner pp WHERE ProjectId = @ProjectId AND PartnerId = @PartnerId AND Rol = @Rol";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("ProjectId", pr.Id);
                cmd.Parameters.AddWithValue("PartnerId", pa.Id);
                cmd.Parameters.AddWithValue("Rol",rol);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }
        public void KoppelPartnerAanProject(Project pr,Partner pa,string rol)
        {
            string query = "INSERT INTO ProjectPartner(ProjectId,PartnerId,Rol) VALUES (@ProjectId,@PartnerId,@Rol)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("ProjectId", pr.Id);
                cmd.Parameters.AddWithValue("PartnerId", pa.Id);
                cmd.Parameters.AddWithValue("Rol", rol);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Partner> HaalAllePartnersMetProjectId(int projectId)
        {
            List<Partner> partners = new();

            string query = "SELECT pr.PartnerId, pr.Naam, pr.Email FROM ProjectPartner pp JOIN Partner pr ON pp.PartnerId=pr.Id WHERE pp.ProjectId=@projectId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("projectId", projectId);
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partners.Add(new Partner(
                            reader.GetInt32(reader.GetOrdinal("PartnerId")),
                            reader.GetString(reader.GetOrdinal("Naam")),
                            reader.GetString(reader.GetOrdinal("Email"))));
                    }
                }
            }
            return partners;
        }
    }
}
