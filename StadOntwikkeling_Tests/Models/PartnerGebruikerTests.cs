using System;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class PartnerGebruikerTests
    {
        [Fact]
        public void Partner_Valid_SetsProperties()
        {
            var p = new Partner("Naam", "a@b.c");
            Assert.Equal("Naam", p.Naam);
            Assert.Equal("a@b.c", p.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Partner_InvalidNaam_Throws(string naam)
        {
            Assert.Throws<PartnerException>(() => new Partner(naam, "a@b.c"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Partner_InvalidEmail_Throws(string email)
        {
            Assert.Throws<PartnerException>(() => new Partner("Naam", email));
        }

        [Fact]
        public void Gebruiker_InvalidEmail_Throws()
        {
            Assert.Throws<PartnerException>(() => new Gebruiker("", false, false));
        }

        [Fact]
        public void Gebruiker_SetInvalidId_Throws()
        {
            var g = new Gebruiker("a@b.c", false, false);
            Assert.Throws<PartnerException>(() => g.Id = 0);
        }
    }
}