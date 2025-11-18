using StadOntwikkeling_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class ProjectPartner
	{
		private int _id;
		private Partner _partner;
		private Project _project;
		//private Rol _rol; ==> kunnen wij enums in de UT's ook testen?

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public Partner Partner
		{
			get { return _partner; }
			set { _partner = value; }
		}
		public Project Project
		{
			get { return _project; }
			set { _project = value; }
		}
	}
}
