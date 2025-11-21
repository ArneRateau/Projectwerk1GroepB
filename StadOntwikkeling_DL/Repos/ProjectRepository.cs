using Microsoft.Data.SqlClient;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StadOntwikkeling_DL.Repos
{
    public class ProjectRepository : IProjectRepository
    {
        //private readonly string _connectionString = "Data Source=LAPTOP-TD9V3TI9;Initial Catalog=GentProjecten;Integrated Security=True;Trust Server Certificate=True";
        private readonly string _connectionString = "Data Source=MRROBOT\\SQLEXPRESS;Initial Catalog=GentProjecten;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

       
        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            string queryProject = @"
                SELECT pr.ProjectId, pr.Titel, pr.Startdatum, pr.Status, pr.Beschrijving,
                       l.LocatieId, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.HuisNummer
                FROM Project pr 
                JOIN Locatie l ON pr.LocatieId = l.LocatieId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                        project.ProjectOnderdelen = LoadProjectOnderdelen(project.Id);

                    return projects;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting projects", ex);
                }
            }
        }

        
        private List<string>  LoadPartnerNames(int projectId)
        {
            List<string> partners = new();

            string query = "SELECT pa.PartnerId, pa.Naam, pa.Email FROM Partner pa JOIN ProjectPartner pp ON pa.PartnerId=pp.PartnerId WHERE pp.ProjectId=@projectId";
            //string query = @"
            //    SELECT pa.Naam
            //    FROM Partner pa
            //    JOIN ProjectPartner pp ON pa.PartnerId = pp.PartnerId
            //    WHERE pp.ProjectId = @ProjectId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectId", projectId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        partners.Add(reader.GetString(reader.GetOrdinal("Naam")));
				}
            }
            return partners;
        }

      
        private List<string> LoadProjectTypes(int projectId)
        {
            List<string> types = new List<string>();

            string query = @"
                SELECT StadsontwikkelingId, GroeneRuimteId, InnovatiefWonenId
                FROM Project_ProjectType
                WHERE ProjectId = @ProjectId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectId", projectId);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        if (r["StadsontwikkelingId"] != DBNull.Value) types.Add("Stadsontwikkeling");
                        if (r["GroeneRuimteId"] != DBNull.Value) types.Add("Groene Ruimte");
                        if (r["InnovatiefWonenId"] != DBNull.Value) types.Add("Innovatief Wonen");
                    }
                }
            }

            return types;
        }

       
        public List<ProjectDTO> GetProjectsLite()
        {
            List<ProjectDTO> list = new List<ProjectDTO>();

            string query = @"
                SELECT pr.ProjectId, pr.Titel, pr.Startdatum, pr.Beschrijving, pr.Status, l.Wijk
                FROM Project pr
                JOIN Locatie l ON pr.LocatieId = l.LocatieId";

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                int id = (int)r["ProjectId"];
                var dto = new ProjectDTO(
                    id,
                    (string)r["Titel"],
                    ((Status)(int)r["Status"]).ToString(),
                    (string)r["Wijk"],
                    (DateTime)r["Startdatum"]
                );

                dto.PartnerNamen = LoadPartnerNames(id);
                dto.ProjectTypes = LoadProjectTypes(id);

                list.Add(dto);
            }

            return list;
        }

      
        private List<ProjectOnderdeel> LoadProjectOnderdelen(int projectId)
        {
            List<ProjectOnderdeel> projectOnderdelen = new List<ProjectOnderdeel>();

            string query = "SELECT * FROM Project_ProjectType WHERE ProjectId = @ProjectId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            projectOnderdelen.Add(LoadStadsOntwikkelingProject((int)reader["StadsontwikkelingId"]));

                        if (reader["GroeneRuimteId"] != DBNull.Value)
                            projectOnderdelen.Add(LoadGroenRuimteProject((int)reader["GroeneRuimteId"]));

                        if (reader["InnovatiefWonenId"] != DBNull.Value)
                            projectOnderdelen.Add(LoadInnovatiefWonenProject((int)reader["InnovatiefWonenId"]));
                    }
                }
            }
            return projectOnderdelen;
        }

      
        private StadsontwikkelingProject LoadStadsOntwikkelingProject(int onderdeelId)
        {
            string query = "SELECT * FROM Stadsontwikkeling WHERE ProjectId = @ProjectOnderdeelId";

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using SqlDataReader reader = cmd.ExecuteReader();
                {
                    if (reader.Read())
                    {
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

            string query = @"
                SELECT b.* 
                FROM Bouwfirma b 
                JOIN Stadsontwikkeling_Bouwfirma sb ON sb.BouwfirmaId = b.BouwfirmaId 
                WHERE sb.ProjectId = @ProjectId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        private GroenRuimteProject LoadGroenRuimteProject(int onderdeelId)
        {
            string query = "SELECT * FROM GroeneRuimte WHERE ProjectId = @ProjectOnderdeelId";

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using SqlDataReader reader = cmd.ExecuteReader();
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

        private List<Faciliteit> LoadFaciliteiten(int projectId)
        {
            List<Faciliteit> faciliteiten = new List<Faciliteit>();

            string query = "SELECT * FROM Faciliteit WHERE ProjectId = @ProjectId";

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);

                using SqlDataReader reader = cmd.ExecuteReader();
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

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectOnderdeelId", onderdeelId);

                using SqlDataReader reader = cmd.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        string rawTypes = reader["WoonvormTypes"] as string ?? "";
                        var woonvormen = rawTypes
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToList();

                        float score = 0;
                        if (reader["ArchitectuurInnovatieScore"] != DBNull.Value)
                            score = Convert.ToSingle(reader["ArchitectuurInnovatieScore"]);

                        var onderdeel = new InnovatiefWonenProject(
                            (int)reader["AantalWooneenheden"],
                            woonvormen,
                            reader["Rondleiding"] != DBNull.Value && (bool)reader["Rondleiding"],
                            reader["Showwoning"] != DBNull.Value && (bool)reader["Showwoning"],
                            score,
                            reader["SamenwerkingErfgoed"] != DBNull.Value && (bool)reader["SamenwerkingErfgoed"],
                            reader["SamenwerkingToerisme"] != DBNull.Value && (bool)reader["SamenwerkingToerisme"]
                        );

                        onderdeel.ProjectOnderdeelId = onderdeelId;
                        return onderdeel;
                    }
                }
            }

            return null;
        }

        
        public void updateProject(Project toUpdate)
        {
            string query = @"
                UPDATE Project 
                SET Titel=@titel, 
                    Startdatum=@startdatum, 
                    Beschrijving=@beschrijving, 
                    Status=@status
                WHERE ProjectId=@projectId";

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                connection.Open();
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@titel", toUpdate.Titel);
                cmd.Parameters.AddWithValue("@startdatum", toUpdate.StartDatum);
                cmd.Parameters.AddWithValue("@beschrijving", toUpdate.Beschrijving);
                cmd.Parameters.AddWithValue("@status", (int)toUpdate.Status);
                cmd.Parameters.AddWithValue("@projectId", toUpdate.Id);

                cmd.ExecuteNonQuery();
            }
        }

        
        public int MaakProjectAlgemeen(string titel, int status, DateTime startdatum, string wijk,
                                       string straat, string gemeente, int postcode, string huisnummer, string beschrijving)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            {
                conn.Open();

                // Insert locatie
                string sqlLocatie = @"
                    INSERT INTO Locatie (Wijk, Straat, Gemeente, Postcode, HuisNummer)
                    VALUES (@Wijk, @Straat, @Gemeente, @Postcode, @HuisNummer);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int locId;
                using (SqlCommand cmd = new SqlCommand(sqlLocatie, conn))
                {
                    cmd.Parameters.AddWithValue("@Wijk", wijk);
                    cmd.Parameters.AddWithValue("@Straat", straat);
                    cmd.Parameters.AddWithValue("@Gemeente", gemeente);
                    cmd.Parameters.AddWithValue("@Postcode", postcode);
                    cmd.Parameters.AddWithValue("@HuisNummer", huisnummer);
                    locId = (int)cmd.ExecuteScalar();
                }

                // Insert project
                string sqlProject = @"
                    INSERT INTO Project (Titel, Startdatum, Beschrijving, Status, LocatieId)
                    VALUES (@Titel, @Startdatum, @Beschrijving, @Status, @LocatieId)
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int proId;
                using (SqlCommand cmd = new SqlCommand(sqlProject, conn))
                {
                    cmd.Parameters.AddWithValue("@Titel", titel);
                    cmd.Parameters.AddWithValue("@Startdatum", startdatum);
                    cmd.Parameters.AddWithValue("@Beschrijving", beschrijving);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@LocatieId", locId);
                    proId = (int)cmd.ExecuteScalar();
                }

                // Insert Project_ProjectType
                string sqlLink = @"
                    INSERT INTO Project_ProjectType (ProjectId)
                    VALUES (@ProjectId)";

                using (SqlCommand cmd = new SqlCommand(sqlLink, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", proId);
                    cmd.ExecuteNonQuery();
                }

                return proId;
            }
        }

        public int MaakProjectStads(int projectId, int vergunStatus, int archWaarde,
                                    int toegankelijkheid, int bezienswaardigheid,
                                    int uitleg, int infowand)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            {
                conn.Open();

                string sql = @"
                    INSERT INTO Stadsontwikkeling (ProjectId, VergunningStatus, ArchitecturieleWaarde,
                                                   Toegankelijkheid, Bezienswaardigheid, Uitlegbord, Infowandeling)
                    VALUES (@ProjectId, @VergunningStatus, @Arch, @Toegang,
                            @Beziens, @Uitleg, @Info)
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@VergunningStatus", vergunStatus);
                    cmd.Parameters.AddWithValue("@Arch", archWaarde);
                    cmd.Parameters.AddWithValue("@Toegang", toegankelijkheid);
                    cmd.Parameters.AddWithValue("@Beziens", bezienswaardigheid);
                    cmd.Parameters.AddWithValue("@Uitleg", uitleg);
                    cmd.Parameters.AddWithValue("@Info", infowand);
                    cmd.ExecuteScalar();
                }

                // Na insertion update Project_ProjectType reference
                string updateType = @"
                    UPDATE Project_ProjectType
                    SET StadsontwikkelingId = @Id
                    WHERE ProjectId = @Pid";

                using (SqlCommand cmd = new SqlCommand(updateType, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", projectId);
                    cmd.Parameters.AddWithValue("@Pid", projectId);
                    cmd.ExecuteNonQuery();
                }

                return projectId;
            }
        }

        void IProjectRepository.AddBouwFirmaAanStads(int newID, int id)
        {
            AddBouwFirmaAanStads(newID, id);
        }

        private void AddBouwFirmaAanStads(int projectId, int bouwfirmaId)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            {
                conn.Open();

                string sql = @"
                    INSERT INTO Stadsontwikkeling_Bouwfirma (ProjectId, BouwfirmaId)
                    VALUES (@ProjectId, @BouwfirmaId)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@BouwfirmaId", bouwfirmaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int MaakProjectGroen(int oppervlak, double biodivers, int aantWandelpad,
                                    int toeristischeRoute, double beoordeling)
        {
            throw new NotImplementedException();
        }

        public int MaakProjectInno(int aantWoone, string woonvormT, int rondl,
                                   int showwon, double archInnoScore,
                                   int samErf, int samToer)
        {
            throw new NotImplementedException();
        }

        
        public Project GetProjectById(int id)
        {
            Project project = null;

            string query = @"
                SELECT pr.ProjectId, pr.Titel, pr.Startdatum, pr.Status, pr.Beschrijving,
                       l.LocatieId, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.HuisNummer
                FROM Project pr
                JOIN Locatie l ON pr.LocatieId = l.LocatieId
                WHERE pr.ProjectId = @ProjectId";

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ProjectId", id);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        if (reader.Read())
                        {
                            Locatie locatie = new Locatie(
                                (int)reader["LocatieId"],
                                (string)reader["Straat"],
                                reader["Postcode"].ToString(),
                                (string)reader["Gemeente"],
                                (string)reader["Wijk"],
                                (string)reader["HuisNummer"]
                            );

                            project = new Project(
                                (int)reader["ProjectId"],
                                (string)reader["Titel"],
                                (DateTime)reader["Startdatum"],
                                (Status)reader["Status"],
                                (string)reader["Beschrijving"],
                                locatie,
                                new List<ProjectPartner>(),
                                new List<ProjectOnderdeel>()
                            );
                        }
                    }

                    if (project == null)
                        throw new Exception($"Project met Id {id} niet gevonden.");

                    project.ProjectOnderdelen = LoadProjectOnderdelen(project.Id);

                    return project;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting project by id", ex);
                }
            }
        }

        public void CombineProjectOnderdeel()
        {
            throw new NotImplementedException();
        }

        public void UpdateProject(Project toUpdate)
        {
            throw new NotImplementedException();
        }

        public int MaakProject()
        {
            throw new NotImplementedException();
        }
        public List<ProjectPartner> GetProjectPartners(int projectId)
        {
            string query = "SELECT pa.PartnerId,pa.Naam,pa.Email, pp.Rol," +
            " pr.ProjectId, pr.Titel,pr.StartDatum,pr.Status,pr.Beschrijving," +
            //" pl.Straat AS PartnerStraat,pl.Postcode AS PartnerPostcode,pl.Gemeente AS PartnerGemeente,pl.Wijk AS PartnerWijk,pl.HuisNummer AS PartnerHuisnummer," +
            " prl.Straat AS ProjectStraat, prl.Straat AS ProjectStraat,prl.Straat AS ProjectStraat,prl.Postcode AS ProjectPostcode,prl.Gemeente AS ProjectGemeente,prl.Wijk AS ProjectWijk,prl.HuisNummer AS ProjectHuisnummer" +
            " FROM Partner pa " +
            " JOIN ProjectPartner pp ON pa.PartnerId = pp.PartnerId " +
            " JOIN Project pr ON pp.ProjectId = pr.ProjectId " +
            //" JOIN Locatie pl ON pa.LocatieId = pl.LocatieId" +
            " JOIN Locatie prl ON pr.LocatieId = prl.LocatieId WHERE pp.ProjectId = @ProjectId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                var partners = new List<ProjectPartner>();

                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Partner locatie
                        /*                        var partnerLocatie = new Locatie(
                                                    (string)reader["PartnerStraat"],
                                                    (string)reader["PartnerPostcode"],
                                                    (string)reader["PartnerGemeente"],
                                                    (string)reader["PartnerWijk"],
                                                    (string)reader["PartnerHuisnummer"]
                                                );
                        */
                        // Partner object
                        var partner = new Partner(
                            (int)reader["PartnerId"],
                            (string)reader["Naam"],
                            (string)reader["Email"]
                        );

                        // Project locatie
                        var projectLocatie = new Locatie(
                            (string)reader["ProjectStraat"],
                            (string)reader["ProjectPostcode"],
                            (string)reader["ProjectGemeente"],
                            (string)reader["ProjectWijk"],
                            (string)reader["ProjectHuisnummer"]
                        );

                        // Project object
                        var project = new Project(
                            (int)reader["ProjectId"],
                            (string)reader["Titel"],
                            (DateTime)reader["StartDatum"],
                            (Status)reader["Status"],
                            (string)reader["Beschrijving"],
                            projectLocatie,
                            null,
                            null    
                        );

                        // De koppeling
                        partners.Add(new ProjectPartner(partner, project, (string)reader["Rol"]));
                    }

                    return partners;
                }
            }
            
        }
    }
}
