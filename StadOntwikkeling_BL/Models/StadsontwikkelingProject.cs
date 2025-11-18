using StadOntwikkeling_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class StadsontwikkelingProject
	{
		private int _projectId;
		private Bouwfirma _bouwfirma;
		private bool _architecturaleWaarde;
		private bool _bezienswaardigheid;
		private bool _uitlegbord;
		private bool _infowandeling;

		public int ProjectId
		{
			get { return _projectId; }
			set { _projectId = value; }
		}
		public VergunningStatus VergunningStatus { get; set; }
		public bool ArchitecturaleWaarde
		{
			get { return _architecturaleWaarde; }
			set { _architecturaleWaarde = value; }
		}
		public bool Bezienswaardigheid
		{
			get { return _bezienswaardigheid; }
			set { _bezienswaardigheid = value; }
		}
		public bool Uitlegbord
		{
			get { return _uitlegbord; }
			set { _uitlegbord = value; }
		}
		public bool InfoWandeling
		{
			get { return _infowandeling; }
			set { _infowandeling = value; }
		}
		public Bouwfirma Bouwfirma
		{
			get { return _bouwfirma; }
			set { _bouwfirma = value; }
		}
	}
}
