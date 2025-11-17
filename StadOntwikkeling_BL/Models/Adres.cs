using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class Adres
	{
		private int _id;
		private string _straat;
		private int _postcode;
		private string _gemeente;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Straat
		{
			get { return _straat; }
			set { _straat = value; }
		}
		public int Postcode
		{
			get { return _postcode; }
			set { _postcode = value; }
		}
		public string Gemeente
		{
			get { return _gemeente; }
			set { _gemeente = value; }
		}
	}
}
