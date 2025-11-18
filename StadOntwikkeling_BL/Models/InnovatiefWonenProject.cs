using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class InnovatiefWonenProject
	{
		private Project _project;
		private int _aantalWooneenheden;
		private int _innovatiescore;

		public Project Project
		{
			get { return _project; }
			set { _project = value; }
		}
		public int AantalWooneenheden
		{
			get { return _aantalWooneenheden; }
			set { _aantalWooneenheden = value; }
		}
		public List<WoonvormTypes> WoonvormTypes { get; set; } = new();
		public bool Rondleiding { get; set; }
		public bool ShowWoning { get; set; }
		public int Innovatiescore
		{
			get { return _innovatiescore; }
			set { _innovatiescore = value; }
		}
		public bool SamenwerkingMetErfgoed { get; set; }
		public bool SamenwerkingMetToerismeGent { get; set; }
	}
}
