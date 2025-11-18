using StadOntwikkeling_BL.Exceptions;
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
		private string _telefoon;

		public Bouwfirma(string naam, string email, string telefoon)
		{
			Naam = naam;
			Email = email;
			Telefoon = telefoon;
		}

		public Bouwfirma(int id, string naam, string email, string telefoon)
		{
			Id = id;
			Naam = naam;
			Email = email;
			Telefoon = telefoon;
		}

		public int Id
		{
			get { return _id; }
			set
			{
				if (value <= 0)
					throw new BouwfirmaException("Id mag niet 0 of negatief zijn");
				_id = value;
			}
		}
		public string Naam
		{
			get { return _naam; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new BouwfirmaException("Naam mag niet leeg of null zijn");
				else if (value.Count() < 2)
					throw new BouwfirmaException("Naam moet minstens 2 letters bevatten");
				else
					_naam = value;
			}
		}
		public string Email
		{
			get { return _email; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new BouwfirmaException("Email mag niet leeg of null zijn");
				_email = value;
			}
		}
		public string Telefoon
		{
			get { return _telefoon; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new BouwfirmaException("Nummer mag niet leeg of null zijn");
				_telefoon = value;
			}
		}
	}
}
