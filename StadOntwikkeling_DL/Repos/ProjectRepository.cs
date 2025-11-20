using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
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
		private string _connectionString;

		public ProjectRepository(string connectionString)
		{
			_connectionString = "Data Source=LAPTOP-TD9V3TI9;Initial Catalog=GentProjecten;Integrated Security=True;Trust Server Certificate=True";
		}
		public List<Project> GetProjects()
		{
			List<Project> projects = new List<Project>();
			// Fixed: Added missing comma after ProjectId, fixed column reference l.LocatieId
			string queryProject = "SELECT pr.ProjectId, pr.Titel, pr.Startdatum, pr.Status, pr.Beschrijving, l.LocatieId, l.Straat, l.Gemeente, l.Postcode, l.Wijk, l.HuisNummer FROM Project pr JOIN Locatie l ON pr.LocatieId = l.LocatieId";

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

		private List<string> LoadPartnerNames(int projectId)
		{
			List<string> partners = new List<string>();

			string query = @"
		SELECT pa.Naam
		FROM Partner pa
		JOIN ProjectPartner pp ON pa.PartnerId = pp.PartnerId
		WHERE pp.ProjectId = @ProjectId";

			using (SqlConnection con = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(query, con))
			{
				con.Open();
				cmd.Parameters.AddWithValue("@ProjectId", projectId);

				using (SqlDataReader r = cmd.ExecuteReader())
				{
					while (r.Read())
						partners.Add((string)r["Naam"]);
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
						if (r["StadsontwikkelingId"] != DBNull.Value)
							types.Add("Stadsontwikkeling");

						if (r["GroeneRuimteId"] != DBNull.Value)
							types.Add("Groene Ruimte");

						if (r["InnovatiefWonenId"] != DBNull.Value)
							types.Add("Innovatief Wonen");
					}
				}
			}

			return types;
		}




		public List<ProjectDTO> GetProjectsLite()
		{
			List<ProjectDTO> list = new List<ProjectDTO>();

			string query = @"
		SELECT 
			pr.ProjectId,
			pr.Titel,
			pr.Startdatum,
			pr.Beschrijving,
			pr.Status,
			l.Wijk
		FROM Project pr
		JOIN Locatie l ON pr.LocatieId = l.LocatieId";

			using SqlConnection con = new SqlConnection(_connectionString);
			using SqlCommand cmd = new SqlCommand(query, con);

			con.Open();

			int Id;
			string Title;
			string Status;
			string Wijk;
			DateTime StartDatum;
			string ProjectType;
			List<string> PartnerNamen;
			using SqlDataReader r = cmd.ExecuteReader();
			while (r.Read())
			{
				ProjectDTO ProjectDTO = new ProjectDTO(Id = (int)r["ProjectId"],
					Title = (string)r["Titel"],
					Status = ((Status)(int)r["Status"]).ToString(),
					Wijk = (string)r["Wijk"],
					StartDatum = (DateTime)r["Startdatum"]);

				ProjectDTO.PartnerNamen = LoadPartnerNames(Id);
				ProjectDTO.ProjectTypes = LoadProjectTypes(Id);

				list.Add(ProjectDTO);

			}

			return list;
		}

		private List<ProjectOnderdeel> LoadProjectOnderdelen(int projectId)
		{
			List<ProjectOnderdeel> projectOnderdelen = new List<ProjectOnderdeel>();
			// Fixed: Added missing FROM keyword
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

			using (SqlConnection connection = new SqlConnection(_connectionString))
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

		// Fixed: Return type should be GroeneRuimteProject, implemented loading logic
		private GroenRuimteProject LoadGroenRuimteProject(int onderdeelId)
		{
			string query = "SELECT * FROM GroeneRuimte WHERE ProjectId = @ProjectOnderdeelId";

			using (SqlConnection connection = new SqlConnection(_connectionString))
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

			using (SqlConnection connection = new SqlConnection(_connectionString))
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

		public void UpdateProject(Project toUpdate)
		{
			string query = "UPDATE Project SET Titel=@titel, Startdatum=@startdatum, Beschrijving=@beschrijving, Status=@status WHERE ProjectId=@projectId";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.Parameters.AddWithValue("@titel", toUpdate.Titel);
				cmd.Parameters.AddWithValue("@startdatum", toUpdate.StartDatum);
				cmd.Parameters.AddWithValue("@beschrijving", toUpdate.Beschrijving);
				cmd.Parameters.AddWithValue("@status", (int)toUpdate.Status);
				cmd.Parameters.AddWithValue("@projectId", toUpdate.Id);

				connection.Open();
				cmd.ExecuteNonQuery();
			}
		}

        public int MaakProjectAlgemeen(string titel, int status, DateTime startdatum,string wijk, string straat, string gemeente,int postcode, string huisnummer, string Beschrijving)
        {
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				int locId,proId=0;
				conn.Open();
				string sqllocatie = @"
			INSERT INTO Locatie (Wijk, Straat, Gemeente, Postcode, HuisNummer)
			VALUES (@Wijk, @Straat, @Gemeente, @Postcode, @HuisNummer);

			SELECT CAST(SCOPE_IDENTITY() AS int);";
				using (SqlCommand cmd = new SqlCommand(sqllocatie, conn))
				{
					cmd.Parameters.AddWithValue("@Wijk", wijk);
                    cmd.Parameters.AddWithValue("@Straat", straat);
                    cmd.Parameters.AddWithValue("@Gemeente", gemeente);
                    cmd.Parameters.AddWithValue("@Postcode", postcode);
                    cmd.Parameters.AddWithValue("@HuisNummer", huisnummer);
					locId = (int)cmd.ExecuteScalar();
                }

				string sql = @"
			INSERT INTO Project (Titel, Startdatum, Beschrijving, Status, LocatieId)
			VALUES (@Titel, @Startdatum, @Beschrijving, @Status, @LocatieId)

			SELECT CAST(SCOPE_IDENTITY() AS int);";
				using (SqlCommand cmdo = new SqlCommand(sql, conn))
				{
					cmdo.Parameters.AddWithValue("@Titel", titel);
                    cmdo.Parameters.AddWithValue("@Startdatum", startdatum);
                    cmdo.Parameters.AddWithValue("@Beschrijving", Beschrijving);
                    cmdo.Parameters.AddWithValue("@Status", status);
                    cmdo.Parameters.AddWithValue("@LocatieId", locId);
                    proId = (int)cmdo.ExecuteScalar();
                }
			return proId;
			}
        }

        public int MaakProjectStads(int vergunStatus, int archWaard, int Toegang, int bezienswaard, int uitlegb, int infoWand)
        {
            throw new NotImplementedException();
        }

        public int MaakProjectGroen(int oppervlak, double biodivers, int aantWandelpad, int toeriWandelr, double beoordeling)
        {
            throw new NotImplementedException();
        }

        public int MaakProjectInno(int aantWoone,string woonvormT, int rondl, int showwon,double archInnoScore, int samErf, int samToer)
        {
            throw new NotImplementedException();
        }
    }
}   