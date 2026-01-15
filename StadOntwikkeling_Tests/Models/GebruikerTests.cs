using System;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class GebruikerTests
    {
        [Fact]
        public void Constructor_Minimal_Works()
        {
            var g = new Gebruiker("user@domain.com", isAdmin: true, isPartner: false);
            Assert.Equal("user@domain.com", g.Email);
            Assert.True(g.IsAdmin);
            Assert.False(g.IsPartner);
            Assert.Null(g.Naam);
        }

        [Fact]
        public void Constructor_WithIdAndName_Works()
        {
            var g = new Gebruiker(3, "a@b.c", false, true, "Jan");
            Assert.Equal(3, g.Id);
            Assert.Equal("a@b.c", g.Email);
            Assert.True(g.IsPartner);
            Assert.Equal("Jan", g.Naam);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_InvalidEmail_Throws(string email)
        {
            Assert.Throws<PartnerException>(() => new Gebruiker(email!, false, false));
        }

        [Fact]
        public void Id_SetInvalid_Throws()
        {
            var g = new Gebruiker("ok@ok.com", false, false);
            Assert.Throws<PartnerException>(() => g.Id = 0);
            Assert.Throws<PartnerException>(() => g.Id = -1);
        }

        [Fact]
        public void Email_SetInvalid_Throws()
        {
            var g = new Gebruiker("ok@ok.com", false, false);
            Assert.Throws<PartnerException>(() => g.Email = "");
            Assert.Throws<PartnerException>(() => g.Email = null!);
        }
    }
}