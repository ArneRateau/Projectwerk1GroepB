using Microsoft.Data.SqlClient;
using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StadOntwikkeling_DL.Repos
{
    public class GebruikerRepo : IGebruikerRepo
    {
        public void MaakGebruiker(string email, bool isAdmin, bool isPartner)
        {

        
            string connectionString = "Data Source=MRROBOT\\SQLEXPRESS;Initial Catalog=GentProjecten;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"INSERT INTO Gebruiker (Email, IsAdmin, IsPartner)
                       VALUES (@Email, @IsAdmin, @IsPartner)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@IsAdmin", isAdmin);
                    cmd.Parameters.AddWithValue("@IsPartner", isPartner);
                     //todo zorg er voor dat nieuwe id gebruikt wordt 
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

