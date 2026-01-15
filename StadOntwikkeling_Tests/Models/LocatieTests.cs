using System;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class LocatieTests
    {
        [Fact]
        public void Create_ValidLocatie_SetsProperties()
        {
            var l = new Locatie("Straat", "9000", "Gent", "Wijk", "12");
            Assert.Equal("Straat", l.Straat);
            Assert.Equal("9000", l.Postcode);
            Assert.Equal("Gent", l.Gemeente);
            Assert.Equal("Wijk", l.Wijk);
            Assert.Equal("12", l.Huisnummer);
        }

        [Theory]
        [InlineData(null, "9000", "Gent", "Wijk", "12")]
        [InlineData("Straat", null, "Gent", "Wijk", "12")]
        [InlineData("Straat", "9000", null, "Wijk", "12")]
        [InlineData("Straat", "9000", "Gent", null, "12")]
        [InlineData("Straat", "9000", "Gent", "Wijk", null)]
        public void Create_InvalidField_Throws(string straat, string postcode, string gemeente, string wijk, string huisnummer)
        {
            Assert.Throws<LocatieException>(() => new Locatie(straat, postcode, gemeente, wijk, huisnummer));
        }

        [Fact]
        public void Set_InvalidId_Throws()
        {
            var l = new Locatie("S", "9000", "G", "W", "1");
            Assert.Throws<LocatieException>(() => l.Id = 0);
        }
    }
}