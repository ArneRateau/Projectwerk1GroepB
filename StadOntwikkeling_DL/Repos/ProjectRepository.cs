using Microsoft.Data.SqlClient;
using StadOntwikkeling_BL.Enums;
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
    public class ProjectRepository : IProjectRepository
    {
        private string connectionString;

        public ProjectRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //nog niet klaar
        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();
            string queryProject = "select pr.Titel,pr.startdatum,pr.status,pr.bescrijving, l.straat, l.gemeente, l.postcode,l.wijk,l.huisnummer from Project pr join Locatie l on pr.LocatieId = l.Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = queryProject;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //projects.Add(new Project((int)reader["id"], (string)reader["titel"], (DateTime)reader["startdatum"], (Status)reader["status"], (string)reader["beschrijving"],(new Locatie((string)reader["straat"], (string)reader["postcode"], (string)reader["gemeente"], (string)reader["wijk"]/*,huisnummer */)/*,List<ProjectPartner> projecten*/ /*,List<ProjectOnderdeel> projectOnderdelen */)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Geef projects", ex);
                }
                return projects;
            }
        }
        //nog niet klaar
        public void CombineProjectOnderdeel()
        {

        }
    }
}
