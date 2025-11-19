using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
            string queryProject = "select pr.ProjectId pr.Titel, pr.Startdatum, pr.Status, pr.Beschrijving, l.LocatieId,l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.Huisnummer from Project pr join Locatie l on pr.LocatieId = l.Id";
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
                            Locatie locatie = new Locatie((int)reader["LocatieId"], (string)reader["Straat"], (string)reader["Postcode"], (string)reader["Gemeente"], (string)reader["Wijk"],(string)reader["Huisnummer"]);
                            Project project = new Project((int)reader["ProjectId"], (string)reader["Titel"], (DateTime)reader["Startdatum"], (Status)reader["Status"], (string)reader["Beschrijving"], locatie, new List<ProjectPartner>(),new List<ProjectOnderdeel>());
                            projects.Add(project);
                        }
                    }
                    foreach (var project in projects)
                    {
                        project.ProjectOnderdelen = LoadProjectOnderdelen(project.Id);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Geef projects", ex);
                }
                return projects;
            }
        }
        private List<ProjectOnderdeel> LoadProjectOnderdelen(int projectId)
        {
            List<ProjectOnderdeel> projectOnderdelen = new List < ProjectOnderdeel>();
            string query = "select * Project_ProjectType where ProjectId = @ProjectId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand()) 
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        int onderdeelId = (int)reader["ProjectId"];
                        if (reader["StadsontwikkelingId"] != DBNull.Value)
                        {
                            projectOnderdelen.Add(LoadStadsOntwikkelingProject((int)reader["StadsontwikkelingId"]));
                        }
                        if (reader["GroeneRuimteId"] != DBNull.Value)
                        {
                            projectOnderdelen.Add(LoadGroenRuimteProject((int)reader["GroeneRuimteId"]));
                        }
                        if (reader["InnovatiefWonenId"] != DBNull.Value)
                        {
                            projectOnderdelen.Add(LoadInnovatiefWonenProject((int)reader["InnovatiefWonenId"]));
                        }
                    }
                }
            }
            return projectOnderdelen;
        }
        private StadsontwikkelingProject LoadStadsOntwikkelingProject(int onderdeelId)
        {
            string query = "select * from Stadsontwikkeling where projectId = @ProjectOnderdeelId";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId",onderdeelId);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var onderdeel = new StadsontwikkelingProject(LoadBouwFirmas(onderdeelId), (VergunningStatus)reader["VergunningStatus"], (bool)reader["ArchitecturieeleWaarde"], (Toegankelijkheid)reader["Toegankelijkheid"], (bool)reader["Bezienswaardigheid"], (bool)reader["Uitlegbord"], (bool)reader["InfoWandeling"]);
                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }
            return null;
        }
        //geeft bouwfirmas
        private List<Bouwfirma> LoadBouwFirmas(int projectId)
        {
            List<Bouwfirma> bouwfirmas = new List<Bouwfirma>();
            string query = "select b.* from Bouwfirma b join Stadsontwikkeling_Bouwfirma sb on sb.BouwfirmaId = b.BouwfirmaId where sb.ProjectId = @ProjectId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bouwfirmas.Add(new Bouwfirma((int)reader["BouwFirmaId"], (string)reader["Naam"], (string)reader["Email"], (string)reader["Telefoon"]));
                    }
                }
            }
            return bouwfirmas;
        }
        //nog niet klaar
        private StadsontwikkelingProject LoadGroenRuimteProject(int onderdeelId)
        {
            string query = "select * from GroeneRuimte where projectId = @ProjectOnderdeelId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //
                    }
                }
            }
            return null;
        }
        //nog niet klaar
        private InnovatiefWonenProject LoadInnovatiefWonenProject(int onderdeelId)
        {
            string query = "select * from InnovatiefWonen where projectId = @ProjectOnderdeelId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var onderdeel = new InnovatiefWonenProject((int)reader["AantalWooneenheden"],/*moet list zijn*/(string)reader["WoonvormTypes"], (bool)reader["Rondleiding"], (bool)reader["Showwoning"], (int)/*(float)*/reader["ArchitectuurInnovatieScore"], (bool)reader["SamenwerkingErGoed"], (bool)reader["SamenwerkingToerisme"]);
                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }
            return null;
        }
        //nog niet klaar
        public void CombineProjectOnderdeel()
        {

        }
    }
}
