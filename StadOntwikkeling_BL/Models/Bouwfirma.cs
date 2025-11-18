using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class Bouwfirma
	{
		private int _id;
		private string _naam;
		private string _email;
        public Bouwfirma(int id, string naam, string email)
        {
            Id = id;
            Naam = naam;
            Email = email;
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Naam
		{
			get { return _naam; }
			set { _naam = value; }
		}
	}
}
