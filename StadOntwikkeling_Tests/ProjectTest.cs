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
	public class ProjectTest
	{
		[Fact]
		public void Valid_Constructor_Params()
		{
			var loc = new Locatie("S", "1", "G", "W", "10");
			var p = new Project("Titel", DateTime.Now, Status.Planning, "Desc", loc, new(), new());
			Assert.Equal("Titel", p.Titel);
		}

		[Fact]
		public void Valid_Constructor_Params_Met_ID()
		{
			var loc = new Locatie("S", "1", "G", "W", "10");
			var p = new Project(1, "T", DateTime.Now, Status.Planning, "D", loc, new(), new());
			Assert.Equal(1, p.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_ID(int id)
		{
			var loc = new Locatie("S", "1", "G", "W", "10");
			var p = new Project("T", DateTime.Now, Status.Planning, "D", loc, new(), new());
			Assert.Throws<ProjectException>(() => p.Id = id);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Titel(string value)
		{
			var loc = new Locatie("S", "1", "G", "W", "10");
			var p = new Project("T", DateTime.Now, Status.Planning, "D", loc, new(), new());
			Assert.Throws<ProjectException>(() => p.Titel = value);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Beschrijving(string value)
		{
			var loc = new Locatie("S", "1", "G", "W", "10");
			var p = new Project("T", DateTime.Now, Status.Planning, "D", loc, new(), new());
			Assert.Throws<ProjectException>(() => p.Beschrijving = value);
		}

		[Fact]
		public void Invalid_Locatie_Null()
		{
			var p = new Project("T", DateTime.Now, Status.Planning, "D", new Locatie("S", "1", "G", "W", "10"), new(), new());
			Assert.Throws<ProjectException>(() => p.Locatie = null);
		}

	}
}
