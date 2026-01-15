using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class BouwfirmaTest
	{
		[Fact]
		public void Valid_Constructor()
		{
			var b = new Bouwfirma("Naam", "email@test.com", "123");
			Assert.Equal("Naam", b.Naam);
			Assert.Equal("email@test.com", b.Email);
			Assert.Equal("123", b.Telefoon);
		}

		[Fact]
		public void Valid_Constructor_ID()
		{
			var b = new Bouwfirma(1, "Naam", "email@test.com", "123");
			Assert.Equal(1, b.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_Constructor_ID(int id)
		{
			var b = new Bouwfirma("Naam", "email@test.com", "123");
			Assert.Throws<BouwfirmaException>(() => b.Id = id);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Invalid_Constructor_Params(string naam)
		{
			var b = new Bouwfirma("Ok", "e", "t");
			Assert.Throws<BouwfirmaException>(() => b.Naam = naam);
		}

		[Fact]
		public void Invalid_Constructor_Naam_Param()
		{
			var b = new Bouwfirma("Ok", "e", "t");
			Assert.Throws<BouwfirmaException>(() => b.Naam = "A");
		}

		[Fact]
		public void Invalid_Constuctor_Email_Param()
		{
			var b = new Bouwfirma("Naam", "email", "123");
			Assert.Throws<BouwfirmaException>(() => b.Email = "");
		}

		[Fact]
		public void Invalid_Constructor_Telefoon_Param()
		{
			var b = new Bouwfirma("Naam", "email", "123");
			Assert.Throws<BouwfirmaException>(() => b.Telefoon = "");
		}
	}
}
