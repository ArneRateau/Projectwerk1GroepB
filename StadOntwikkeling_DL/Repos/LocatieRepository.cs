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
    public class LocatieRepository : ILocatieRepository
    {
        private string _connectionString = "Data Source=LAPTOP-TD9V3TI9;Initial Catalog=GentProjecten;Integrated Security=True;Trust Server Certificate=True";

        public LocatieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void updateLocatie(Locatie locatie)
        {
            string query = @"UPDATE Locatie 
                     SET Straat=@straat,
                         HuisNummer=@huisnummer,
                         Postcode=@postcode,
                         Gemeente=@gemeente,
                         Wijk=@wijk
                     WHERE LocatieId=@locatieId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@straat", locatie.Straat);
                cmd.Parameters.AddWithValue("@huisnummer", locatie.Huisnummer);
                cmd.Parameters.AddWithValue("@postcode", locatie.Postcode);
                cmd.Parameters.AddWithValue("@gemeente", locatie.Gemeente);
                cmd.Parameters.AddWithValue("@wijk", locatie.Wijk);
                cmd.Parameters.AddWithValue("@locatieId", locatie.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
