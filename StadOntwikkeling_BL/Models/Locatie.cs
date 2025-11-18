using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class Locatie
	{
		private int _id;
		private string _straat;
		private string _postcode;
		private string _gemeente;
		private string _wijk;

		public Locatie(string straat, string postcode, string gemeente, string wijk)
		{
			Straat = straat;
			Postcode = postcode;
			Gemeente = gemeente;
			Wijk = wijk;
		}

		public Locatie(int id, string straat, string postcode, string gemeente, string wijk)
		{
			Id = id;
			Straat = straat;
			Postcode = postcode;
			Gemeente = gemeente;
			Wijk = wijk;
		}

		public int Id
		{
			get { return _id; }
			set
			{
				if (value <= 0)
					throw new LocatieException("Id mag niet 0 of negatief zijn");
				_id = value;
			}
		}
		public string Straat
		{
			get { return _straat; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new LocatieException("Straat mag niet leeg of null zijn");
				_straat = value;
			}
		}
		public string Postcode
		{
			get { return _postcode; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new LocatieException("Postcode mag niet leeg of null zijn");
				_postcode = value;
			}
		}
		public string Gemeente
		{
			get { return _gemeente; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new LocatieException("Gemeente mag niet leeg of null zijn");
				_gemeente = value;
			}
		}
		public string Wijk
		{
			get { return _wijk; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new LocatieException("Wijk mag niet leeg of null zijn");
				_wijk = value;
			}
		}
	}
}
