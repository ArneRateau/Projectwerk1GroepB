using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class ProjectPartnerTest
	{
		[Fact]
		public void Valid_Constructor()
		{
			var partner = new Partner("Naam", "mail");
			var project = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			var pp = new ProjectPartner(partner, project, "Rol");
			Assert.Equal("Rol", pp.Rol);
		}

		[Fact]
		public void Valid_Constructor_Met_ID()
		{
			var partner = new Partner("Naam", "mail");
			var project = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			var pp = new ProjectPartner(1, partner, project, "Rol");
			Assert.Equal(1, pp.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_ID(int id)
		{
			var partner = new Partner("Naam", "mail");
			var project = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			var pp = new ProjectPartner(partner, project, "Rol");
			Assert.Throws<ProjectPartnerException>(() => pp.Id = id);
		}

		[Fact]
		public void Invalid_Partner_Null()
		{
			var project = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			var pp = new ProjectPartner(new Partner("Naam", "mail"), project, "Rol");
			Assert.Throws<ProjectPartnerException>(() => pp.Partner = null);
		}

		[Fact]
		public void Invalid_Project_Null()
		{
			var partner = new Partner("Naam", "mail");
			var pp = new ProjectPartner(partner, new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new()), "Rol");
			Assert.Throws<ProjectPartnerException>(() => pp.Project = null);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Rol(string rol)
		{
			var partner = new Partner("Naam", "mail");
			var project = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			var pp = new ProjectPartner(partner, project, "Rol");
			Assert.Throws<ProjectException>(() => pp.Rol = rol);
		}

	}
}
