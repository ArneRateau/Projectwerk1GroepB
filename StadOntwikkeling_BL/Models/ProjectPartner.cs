using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Exceptions;
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
		private string _rol;
        public string Naam { get; set; }
        public ProjectPartner(Partner partner, Project project, string rol)
		{
			Partner = partner;
			Project = project;
			Rol = rol;
		}

		public ProjectPartner(int id, Partner partner, Project project, string rol)
		{
			Id = id;
			Partner = partner;
			Project = project;
			Rol = rol;
		}
		public ProjectPartner(int id, Partner partner, Project project, string rol, string naam)
		{
			Id = id;
			Partner = partner;
			Project = project;
			Rol = rol;
			Naam = naam;

        }
        public int Id
		{
			get { return _id; }
			set
			{
				if (value <= 0)
					throw new ProjectPartnerException("Id mag niet nul of negatief zijn");
				_id = value;
			}
		}
		public Partner Partner
		{
			get { return _partner; }
			set
			{
				if (value is null)
					throw new ProjectPartnerException("Partner mag niet null zijn");
				_partner = value;
			}
		}
		public Project Project
		{
			get { return _project; }
			set
			{
				if (value is null)
					throw new ProjectPartnerException("Project mag niet null zijn");
				_project = value;
			}
		}
		public string Rol
		{
			get { return _rol; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ProjectException("Rol mag niet leeg of null zijn");
				_rol = value;
			}
		}

	}
}
