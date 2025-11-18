using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class StadsontwikkelingProject : ProjectOnderdeel
	{
		private Project _project;

		public StadsontwikkelingProject(Project project, List<Bouwfirma> bouwfirmas, VergunningStatus vergunningStatus, bool architecturieeleWaarde, Toegankelijkheid toegankelijkheid, bool bezienswaardigheid, bool uitlegbord, bool infoWandeling)
		{
			Project = project;
			Bouwfirmas = bouwfirmas;
			VergunningStatus = vergunningStatus;
			ArchitecturieeleWaarde = architecturieeleWaarde;
			Toegankelijkheid = toegankelijkheid;
			Bezienswaardigheid = bezienswaardigheid;
			Uitlegbord = uitlegbord;
			InfoWandeling = infoWandeling;
		}

		public Project Project
		{
			get { return _project; }
			set
			{
				if (value is null)
					throw new StadsontwikkelingProjectException("Project mag niet null zijn");
				_project = value;
			}
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
