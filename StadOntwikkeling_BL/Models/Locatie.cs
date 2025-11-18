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
		private string _wijk;
        private string _straat;
        private int _postcode;
        private string _gemeente;
        private string _huisnummer;

        public Locatie(int id, string wijk, string straat, int postcode, string gemeente, string huisnummer)
        {
            Id = id;
            Wijk = wijk;
            Straat = straat;
            Postcode = postcode;
            Gemeente = gemeente;
            Huisnummer = huisnummer;
        }

        public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Wijk
		{
			get { return _wijk; }
			set { _wijk = value; }
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
        public string Huisnummer
        {
            get { return _huisnummer; }
            set { _huisnummer = value; }
        }
    }
}
