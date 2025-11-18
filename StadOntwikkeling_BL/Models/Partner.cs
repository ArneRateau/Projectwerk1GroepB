using StadOntwikkeling_BL.Exceptions;
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

		public Partner(string naam, Locatie locatie, string email, List<ProjectPartner> projecten)
		{
			Naam = naam;
			Locatie = locatie;
			Email = email;
			Projecten = projecten;
		}

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
			set
			{
				if (value <= 0)
					throw new PartnerException("Id mag niet nul of negatief zijn");
				_id = value;
			}
		}
		public string Naam
		{
			get { return _naam; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new PartnerException("Naam mag niet leeg of null zijn");
				else if (value.Count() < 2)
					throw new PartnerException("Naam moet minstens 2 letters bevatten");
				else
					_naam = value;
			}
		}
		public Locatie Locatie
		{
			get { return _locatie; }
			set
			{
				if (value == null)
					throw new PartnerException("Locatie mag niet null zijn");
				_locatie = value;
			}
		}
		public string Email
		{
			get { return _email; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new PartnerException("Email mag niet leeg of null zijn");
				_email = value;
			}
		}
		public List<ProjectPartner> Projecten { get; set; } = new();
	}
}
