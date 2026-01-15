using System;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class PartnerTests
    {
        [Fact]
        public void Constructor_ValidValues_SetsProperties()
        {
            var p = new Partner("Stad Gent", "contact@gent.be");
            Assert.Equal("Stad Gent", p.Naam);
            Assert.Equal("contact@gent.be", p.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_InvalidNaam_Throws(string naam)
        {
            Assert.Throws<PartnerException>(() => new Partner(naam!, "a@b.c"));
        }

        [Fact]
        public void Constructor_TooShortNaam_Throws()
        {
            Assert.Throws<PartnerException>(() => new Partner("A", "a@b.c"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_InvalidEmail_Throws(string email)
        {
            Assert.Throws<PartnerException>(() => new Partner("Valid", email!));
        }

        [Fact]
        public void Id_SetValidAndInvalid()
        {
            var p = new Partner("Valid", "v@x.com");
            p.Id = 5;
            Assert.Equal(5, p.Id);
            Assert.Throws<PartnerException>(() => p.Id = 0);
            Assert.Throws<PartnerException>(() => p.Id = -2);
        }
    }
}