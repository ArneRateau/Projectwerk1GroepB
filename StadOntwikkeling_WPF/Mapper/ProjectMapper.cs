using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Mapper
{
	public class ProjectMapper
	{
		public Project MapToDomain(ProjectUI p)
		{
			throw new NotImplementedException();
            //return new Project(p.Id, p.Titel, p.StartDatum, p.Status, p.Beschrijving, LocatieMapper.MapToDomain(p.Locatie), p.Projecten, p.ProjectOnderdelen);
        }
		public Project MapFromDomain(Project p)
		{
            throw new NotImplementedException();
            //return new ProjectUI(p.Id, p.Titel, p.StartDatum, p.Status, p.Beschrijving, LocatieMapper.MapFromDomain(p.Locatie), ProjectPartner.Ma, p.ProjectOnderdelen);
        }
	}
}
