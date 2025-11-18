using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class GroenRuimteProject : ProjectOnderdeel
	{
		private double _oppervlakte;
		private double _biodiversiteitscore;
		private int _aantalWandelpaden;
		private bool _inWandelroutes;
		private double _beoordeling;

		public GroenRuimteProject(double oppervlakte, double biodiversiteitscore, int aantalWandelpaden, List<string> faciliteiten, bool inWandelroutes, double beoordeling)
		{
			Oppervlakte = oppervlakte;
			Biodiversiteitscore = biodiversiteitscore;
			AantalWandelpaden = aantalWandelpaden;
			Faciliteiten = faciliteiten;
			InWandelroutes = inWandelroutes;
			Beoordeling = beoordeling;
		}

		public double Oppervlakte
		{
			get { return _oppervlakte; }
			set
			{
				if (value <= 0)
					throw new GroenRuimteProjectException("Oppervlakte mag niet nul of negatief zijn");
				_oppervlakte = value;
			}
		}
		public double Biodiversiteitscore
		{
			get { return _biodiversiteitscore; }
			set
			{
				if (value < 0)
					throw new GroenRuimteProjectException("Biodiversiteitscore mag niet negatief zijn");
				_biodiversiteitscore = value;
			}
		}
		public int AantalWandelpaden
		{
			get { return _aantalWandelpaden; }
			set
			{
				if (value < 0)
					throw new GroenRuimteProjectException("aantal wandelpaden kan niet negatief zijn");
				_aantalWandelpaden = value;
			}
		}
		public List<string> Faciliteiten { get; set; } = new();
		public bool InWandelroutes
		{
			get { return _inWandelroutes; }
			set { _inWandelroutes = value; }
		}
		public double Beoordeling
		{
			get { return _beoordeling; }
			set { _beoordeling = value; }
		}
	}
}
