using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Mapper
{
	public class ProjectPartnerMapper
	{
		public ProjectPartnerUI MapFromDomain(ProjectPartner p)
		{
			return new ProjectPartnerUI(p.Id, PartnerMapper.)
		}
		public ProjectPartner MapToDomain(ProjectPartnerUI p)
		{

		}
	}
}
