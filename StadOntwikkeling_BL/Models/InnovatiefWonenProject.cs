using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class InnovatiefWonenProject
	{
		private string _woonvormTypes;
        private int _aantalWooneenheden;
		private double _innovatiescore;
		private bool _rondleiding;
		private bool _showWoning;
		private bool _samenwerkingMetErfgoed;
        private bool _samenwerkingMetToerismeGent;


       
		public int AantalWooneenheden
		{
			get { return _aantalWooneenheden; }
			set { _aantalWooneenheden = value; }
		}
		public bool Rondleiding { get; set; }
		public bool ShowWoning { get; set; }
		
		public bool SamenwerkingMetErfgoed { get; set; }
		public bool SamenwerkingMetToerismeGent { get; set; }
        public string WoonvormTypes
        {
            get { return _woonvormTypes; }
            set { _woonvormTypes = value; }
        }
        public double Innovatiescore {
            get { return _innovatiescore; }
            set { _innovatiescore = value; }
        }
    }
}
