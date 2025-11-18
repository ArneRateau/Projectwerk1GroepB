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

        public GroentRuimteProject(int projectId, double oppervlakte, int biodiversiteitscore, int aantalWandelpaden, bool inWandelroutes, int beoordeling)
        {
            ProjectId = projectId;
            Oppervlakte = oppervlakte;
            Biodiversiteitscore = biodiversiteitscore;
            AantalWandelpaden = aantalWandelpaden;
            InWandelroutes = inWandelroutes;
            Beoordeling = beoordeling;
        }

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
