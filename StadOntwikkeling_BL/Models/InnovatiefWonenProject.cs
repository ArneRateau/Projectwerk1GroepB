using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class InnovatiefWonenProject : ProjectOnderdeel
	{
		private Project _project;
		private int _aantalWooneenheden;
		private int _innovatiescore;

		public InnovatiefWonenProject(Project project, int aantalWooneenheden, List<string> woonvormTypes, bool rondleiding, bool showWoning, int innovatiescore, bool samenwerkingMetErfgoed, bool samenwerkingMetToerismeGent)
		{
			Project = project;
			AantalWooneenheden = aantalWooneenheden;
			WoonvormTypes = woonvormTypes;
			Rondleiding = rondleiding;
			ShowWoning = showWoning;
			Innovatiescore = innovatiescore;
			SamenwerkingMetErfgoed = samenwerkingMetErfgoed;
			SamenwerkingMetToerismeGent = samenwerkingMetToerismeGent;
		}

		public Project Project
		{
			get { return _project; }
			set
			{
				if (value is null)
					throw new InnovatiefWonenProjectException("Project mag niet null zijn");
				_project = value;
			}
		}
		public int AantalWooneenheden
		{
			get { return _aantalWooneenheden; }
			set
			{
				if (value < 0)
					throw new InnovatiefWonenProjectException("aantal wooneenheden kan niet negatief zijn");
				_aantalWooneenheden = value;
			}
		}
		public List<string> WoonvormTypes { get; set; } = new();
		public bool Rondleiding { get; set; }
		public bool ShowWoning { get; set; }
		public int Innovatiescore
		{
			get { return _innovatiescore; }
			set
			{
				if (value < 0)
					throw new InnovatiefWonenProjectException("Innovatiescore kan niet negatief zijn");
				_innovatiescore = value;
			}
		}
		public bool SamenwerkingMetErfgoed { get; set; }
		public bool SamenwerkingMetToerismeGent { get; set; }
	}
}
