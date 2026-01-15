using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class LocatieTest
	{
		[Fact]
		public void Valid_Constructor_Zonder_ID()
		{
			var l = new Locatie("Straat", "1000", "Gent", "Wijk", "12");
			Assert.Equal("Straat", l.Straat);
		}

		[Fact]
		public void Valid_Constructor_Met_ID()
		{
			var l = new Locatie(1, "S", "1", "G", "W", "10");
			Assert.Equal(1, l.Id);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_Constructor_ID(int id)
		{
			var l = new Locatie("S", "1", "G", "W", "10");
			Assert.Throws<LocatieException>(() => l.Id = id);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Invalid_Constructor_Params(string invalid)
		{
			var l = new Locatie("S", "1", "G", "W", "10");

			Assert.Throws<LocatieException>(() => l.Straat = invalid);
			Assert.Throws<LocatieException>(() => l.Postcode = invalid);
			Assert.Throws<LocatieException>(() => l.Gemeente = invalid);
			Assert.Throws<LocatieException>(() => l.Wijk = invalid);
			Assert.Throws<LocatieException>(() => l.Huisnummer = invalid);
		}
	}
}
