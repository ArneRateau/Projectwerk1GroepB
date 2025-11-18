using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class Faciliteit
	{
		private int _id;
		private string _naam;
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
