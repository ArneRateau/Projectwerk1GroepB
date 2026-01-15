using System;
using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class BouwfirmaTests
    {
        [Fact]
        public void Create_ValidBouwfirma_SetsProperties()
        {
            var b = new Bouwfirma("BAM", "info@bam.be", "09 123 45 67");
            Assert.Equal("BAM", b.Naam);
            Assert.Equal("info@bam.be", b.Email);
            Assert.Equal("09 123 45 67", b.Telefoon);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_InvalidEmail_Throws(string email)
        {
            Assert.Throws<BouwfirmaException>(() => new Bouwfirma("BAM", email, "09"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("A")]
        public void Create_InvalidNaam_Throws(string naam)
        {
            Assert.Throws<BouwfirmaException>(() => new Bouwfirma(naam, "e@x.com", "09"));
        }

        [Fact]
        public void Set_InvalidId_Throws()
        {
            Assert.Throws<BouwfirmaException>(() => new Bouwfirma(0, "Valid", "a@b.c", "09"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_InvalidTelefoon_Throws(string tel)
        {
            Assert.Throws<BouwfirmaException>(() => new Bouwfirma("Valid", "a@b.c", tel));
        }
    }
}