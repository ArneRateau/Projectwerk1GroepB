using Microsoft.Data.SqlClient;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace StadOntwikkeling_DL.Repos
{
	public class GebruikerRepository : IGebruikerRepository
	{
		private string _connectionString = "Data Source=MRROBOT\\SQLEXPRESS;Initial Catalog = GentProjecten; Integrated Security = True; Encrypt=True;Trust Server Certificate=True";



        public GebruikerRepository(string connectionstring)
		{
			_connectionString = connectionstring;
		}

		public int MaakGebruiker(string email, bool isAdmin, bool isPartner)
		{
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
            INSERT INTO Gebruiker (Email, IsAdmin, IsPartner)
            VALUES (@Email, @IsAdmin, @IsPartner);

            SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@IsAdmin", isAdmin);
                    cmd.Parameters.AddWithValue("@IsPartner", isPartner);

                    int newId = (int)cmd.ExecuteScalar();
                    return newId;
                }
            }
        }

        public Gebruiker? ZoekGebruikerMetEmail(string email)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				string query = @"SELECT * FROM Gebruiker WHERE Email = @email";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@email", email);

					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							int id = reader.GetInt32(0);
							string userEmail = reader.GetString(1);
							bool isAdmin = reader.GetBoolean(2);
							bool isPartner = reader.GetBoolean(3);
							Gebruiker nieuwGebruiker = new(id, userEmail, isAdmin, isPartner);
							return nieuwGebruiker;
						}
					}
				}
			}	
			return null;
		}
	}
}

