using StadOntwikkeling_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class StadsontwikkelingProject : ProjectOnderdeel
	{
		public StadsontwikkelingProject(List<Bouwfirma> bouwfirmas, VergunningStatus vergunningStatus, bool architecturieeleWaarde, Toegankelijkheid toegankelijkheid, bool bezienswaardigheid, bool uitlegbord, bool infoWandeling)
		{
			Bouwfirmas = bouwfirmas;
			VergunningStatus = vergunningStatus;
			ArchitecturieeleWaarde = architecturieeleWaarde;
			Toegankelijkheid = toegankelijkheid;
			Bezienswaardigheid = bezienswaardigheid;
			Uitlegbord = uitlegbord;
			InfoWandeling = infoWandeling;
		}

		public List<Bouwfirma> Bouwfirmas { get; set; } = new();
		public VergunningStatus VergunningStatus { get; set; }
		public bool ArchitecturieeleWaarde { get; set; }
		public Toegankelijkheid Toegankelijkheid { get; set; }
		public bool Bezienswaardigheid { get; set; }
		public bool Uitlegbord { get; set; }
		public bool InfoWandeling { get; set; }
	}
}
