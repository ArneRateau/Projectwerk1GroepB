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
		private Adres _adres;
		private string _email;

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
		public Adres Adres
		{
			get { return _adres; }
			set { _adres = value; }
		}
		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}
		public List<ProjectPartner> Projecten { get; set; } = new();
	}
}
