using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class PartnerTest
	{
		[Fact]
		public void Valid_Constructor_Params()
		{
			var p = new Partner("Naam", "email@test.com");
			Assert.Equal("Naam", p.Naam);
		}

		[Fact]
		public void Valid_Constructor_Params_With_ID()
		{
			var p = new Partner(1, "Naam", "mail");
			Assert.Equal(1, p.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_ID(int id)
		{
			var p = new Partner("Naam", "email@test.com");
			Assert.Throws<PartnerException>(() => p.Id = id);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Lege_Naam(string naam)
		{
			var p = new Partner("AB", "email@test.com");
			Assert.Throws<PartnerException>(() => p.Naam = naam);
		}

		[Fact]
		public void Invalid_Naam_TeKort()
		{
			var p = new Partner("Ok", "email@test.com");
			Assert.Throws<PartnerException>(() => p.Naam = "A");
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Email(string email)
		{
			var p = new Partner("Naam", "email@test.com");
			Assert.Throws<PartnerException>(() => p.Email = email);
		}

	}
}
