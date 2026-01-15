using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class GebruikerTest
	{

		[Fact]
		public void Valid_Constructor_params()
		{
			var gebruiker = new Gebruiker("test@example.com", true, false);

			Assert.Equal("test@example.com", gebruiker.Email);
			Assert.True(gebruiker.IsAdmin);
			Assert.False(gebruiker.IsPartner);
			Assert.Null(gebruiker.Naam);
		}

		[Fact]
		public void Valid_Constructor_Met_ID()
		{
			var gebruiker = new Gebruiker(1, "test@example.com", false, true);
			Assert.Equal(1, gebruiker.Id);
			Assert.Equal("test@example.com", gebruiker.Email);
			Assert.False(gebruiker.IsAdmin);
			Assert.True(gebruiker.IsPartner);
		}

		[Fact]
		public void Valid_Construtor_Met_Naam()
		{
			var gebruiker = new Gebruiker(5, "email@test.com", true, true, "Ali");

			Assert.Equal(5, gebruiker.Id);
			Assert.Equal("email@test.com", gebruiker.Email);
			Assert.Equal("Ali", gebruiker.Naam);
			Assert.True(gebruiker.IsAdmin);
			Assert.True(gebruiker.IsPartner);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-50)]
		public void Constructor_WithInvalidId_ShouldThrow(int invalid)
		{
			Assert.Throws<PartnerException>(
				() => new Gebruiker(invalid, "email@test.com", false, false)
			);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Invalid_Email(string invalidEmail)
		{
			var gebruiker = new Gebruiker("temp@test.com", false, false);

			Assert.Throws<PartnerException>(() => gebruiker.Email = invalidEmail);
		}

		[Fact]
		public void Valid_Email()
		{
			var gebruiker = new Gebruiker("original@test.com", false, false);
			gebruiker.Email = "new@test.com";

			Assert.Equal("new@test.com", gebruiker.Email);
		}

		[Fact]
		public void Valid_Naam()
		{
			var gebruiker = new Gebruiker("test@test.com", false, false);
			gebruiker.Naam = "Ali";

			Assert.Equal("Ali", gebruiker.Naam);
		}

		[Fact]
		public void Valid_Setting_Van_Boolean_Waardes()
		{
			var gebruiker = new Gebruiker("person@example.com", true, false);

			gebruiker.IsAdmin = false;
			gebruiker.IsPartner = true;

			Assert.False(gebruiker.IsAdmin);
			Assert.True(gebruiker.IsPartner);
		}
	}
}
