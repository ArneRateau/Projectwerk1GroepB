using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class Partner
	{
		private int _id;
		private string _naam;
		private Locatie _locatie;
		private string _email;

        public Partner(int id, string naam, Locatie locatie, string email, List<ProjectPartner> projecten)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            Email = email;
            Projecten = projecten;
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
		public Locatie Locatie
		{
			get { return _locatie; }
			set { _locatie = value; }
		}
		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}
		public List<ProjectPartner> Projecten { get; set; } = new();
	}
}
