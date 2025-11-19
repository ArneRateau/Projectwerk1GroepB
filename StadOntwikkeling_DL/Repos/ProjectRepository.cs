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
        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();
            // Fixed: Added missing comma after ProjectId, fixed column reference l.LocatieId
            string queryProject = "SELECT pr.ProjectId, pr.Titel, pr.Startdatum, pr.Status, pr.Beschrijving, l.LocatieId, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.HuisNummer FROM Project pr JOIN Locatie l ON pr.LocatieId = l.LocatieId";

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
                            // Fixed: Postcode is INT in database, convert to string for display
                            Locatie locatie = new Locatie(
                                (int)reader["LocatieId"],
                                (string)reader["Straat"],
                                reader["Postcode"].ToString(),
                                (string)reader["Gemeente"],
                                (string)reader["Wijk"],
                                (string)reader["HuisNummer"]
                            );

                            Project project = new Project(
                                (int)reader["ProjectId"],
                                (string)reader["Titel"],
                                (DateTime)reader["Startdatum"],
                                (Status)reader["Status"],
                                (string)reader["Beschrijving"],
                                locatie,
                                new List<ProjectPartner>(),
                                new List<ProjectOnderdeel>()
                            );

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
                    throw new Exception("Error getting projects", ex);
                }
                return projects;
            }
        }

        public List<Project> GetProjectsLite()
        {
            List<Project> projects = new List<Project>();

            string query = @"
        SELECT 
            pr.ProjectId,
            pr.Titel,
            pr.Startdatum,
            pr.Status,
            pr.Beschrijving,
            l.LocatieId,
            l.Straat,
            l.Gemeente,
            l.Postcode,
            l.Wijk,
            l.HuisNummer
        FROM Project pr
        JOIN Locatie l ON pr.LocatieId = l.LocatieId";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        Locatie locatie = new Locatie(
                    (int)r["LocatieId"],
                    (string)r["Wijk"],
                    (string)r["Straat"],
                    (string)r["Gemeente"],
                    (string)r["Postcode"],
                    (string)r["HuisNummer"]
                );

                        Project p = new Project(
                            (int)r["ProjectId"],
                            (string)r["Titel"],
                            (DateTime)r["Startdatum"],
                            (Status)(int)r["Status"],
                            (string)r["Beschrijving"],
                            locatie,
                            new List<ProjectPartner>(),  // empty
                            new List<ProjectOnderdeel>() // empty
                        );

                        projects.Add(p);
                    }
                }
            }

            return projects;
        }

        private List<ProjectOnderdeel> LoadProjectOnderdelen(int projectId)
        {
            List<ProjectOnderdeel> projectOnderdelen = new List<ProjectOnderdeel>();
            // Fixed: Added missing FROM keyword
            string query = "SELECT * FROM Project_ProjectType WHERE ProjectId = @ProjectId";

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
            string query = "SELECT * FROM Stadsontwikkeling WHERE ProjectId = @ProjectOnderdeelId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query; // Fixed: Added missing CommandText assignment
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Fixed: Column name is "ArchitecturieleWaarde" not "ArchitecturieeleWaarde"
                        var onderdeel = new StadsontwikkelingProject(
                            LoadBouwFirmas(onderdeelId),
                            (VergunningStatus)reader["VergunningStatus"],
                            (bool)reader["ArchitecturieleWaarde"],
                            (Toegankelijkheid)reader["Toegankelijkheid"],
                            (bool)reader["Bezienswaardigheid"],
                            (bool)reader["Uitlegbord"],
                            (bool)reader["Infowandeling"]
                        );
                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }
            return null;
        }

        private List<Bouwfirma> LoadBouwFirmas(int projectId)
        {
            List<Bouwfirma> bouwfirmas = new List<Bouwfirma>();
            string query = "SELECT b.* FROM Bouwfirma b JOIN Stadsontwikkeling_Bouwfirma sb ON sb.BouwfirmaId = b.BouwfirmaId WHERE sb.ProjectId = @ProjectId";

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
                        bouwfirmas.Add(new Bouwfirma(
                            (int)reader["BouwfirmaId"],
                            (string)reader["Naam"],
                            (string)reader["Email"],
                            (string)reader["Telefoon"]
                        ));
                    }
                }
            }
            return bouwfirmas;
        }

        // Fixed: Return type should be GroeneRuimteProject, implemented loading logic
        private GroenRuimteProject LoadGroenRuimteProject(int onderdeelId)
        {
            string query = "SELECT * FROM GroeneRuimte WHERE ProjectId = @ProjectOnderdeelId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query; // Fixed: Added missing CommandText assignment
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var faciliteiten = LoadFaciliteiten(onderdeelId)
                    .Select(f => f.Naam)
                    .ToList();
                        var onderdeel = new GroenRuimteProject(
                            (double)reader["Oppervlakte"],
                            (double)reader["Biodiversiteit"],
                            (int)reader["AantalWandelpaden"],
                            faciliteiten,
                            (bool)reader["InToeristischeWandelroute"],
                            (double)reader["Beoordeling"]
                        );
                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }
            return null;
        }

        // Helper method to load facilities for GroeneRuimte
        private List<Faciliteit> LoadFaciliteiten(int projectId)
        {
            List<Faciliteit> faciliteiten = new List<Faciliteit>();
            string query = "SELECT * FROM Faciliteit WHERE ProjectId = @ProjectId";

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
                        faciliteiten.Add(new Faciliteit(
                            (int)reader["FaciliteitId"],
                            (string)reader["Naam"]
                        ));
                    }
                }
            }
            return faciliteiten;
        }

        private InnovatiefWonenProject LoadInnovatiefWonenProject(int onderdeelId)
        {
            string query = "SELECT * FROM InnovatiefWonen WHERE ProjectId = @ProjectOnderdeelId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = query; // Fixed: Added missing CommandText assignment
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string rawWoonvormTypes = (string)reader["WoonvormTypes"];

                        var woonvormen = rawWoonvormTypes
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToList();

                        var onderdeel = new InnovatiefWonenProject(
                            (int)reader["AantalWooneenheden"],
                            woonvormen,
                            (bool)reader["Rondleiding"],
                            (bool)reader["Showwoning"],
                            (float)reader["ArchitectuurInnovatieScore"],
                            (bool)reader["SamenwerkingErfgoed"],
                            (bool)reader["SamenwerkingToerisme"]
                        );
                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }
            return null;
        }

        // TODO: Implement this method based on your business logic
        public void CombineProjectOnderdeel()
        {
            // Implementation needed based on requirements
        }
    }
}