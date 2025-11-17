using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class GroentRuimteProject
	{
		private int _projectId;
		private double _oppervlakte;
		private int _biodiversiteitscore;
		private int _aantalWandelpaden;
		private bool _inWandelroutes;
		private int _beoordeling;

		public int ProjectId
		{
			get { return _projectId; }
			set { _projectId = value; }
		}
		public double Oppervlakte
		{
			get { return _oppervlakte; }
			set { _oppervlakte = value; }
		}
		public int Biodiversiteitscore
		{
			get { return _biodiversiteitscore; }
			set { _biodiversiteitscore = value; }
		}
		public int AantalWandelpaden
		{
			get { return _aantalWandelpaden; }
			set { _aantalWandelpaden = value; }
		}
		public List<Faciliteit> Faciliteiten { get; set; } = new();
		public bool InWandelroutes
		{
			get { return _inWandelroutes; }
			set { _inWandelroutes = value; }
		}
		public int Beoordeling
		{
			get { return _beoordeling; }
			set { _beoordeling = value; }
		}
	}
}
