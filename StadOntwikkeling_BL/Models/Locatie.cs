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
		private Adres _adres;
		private string _wijk;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public Adres Adres
		{
			get { return _adres; }
			set { _adres = value; }
		}
		public string Wijk
		{
			get { return _wijk; }
			set { _wijk = value; }
		}
	}
}
