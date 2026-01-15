using System;
using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class InnovatiefWonenProjectTests
    {
        [Fact]
        public void Create_Valid_InnovatieProject()
        {
            var i = new InnovatiefWonenProject(10, new List<string> { "Cohousing" }, true, true, 5.5f, true, false);
            Assert.Equal(10, i.AantalWooneenheden);
            Assert.Equal(5.5f, i.Innovatiescore);
            Assert.Contains("Cohousing", i.WoonvormTypes);
        }

        [Fact]
        public void Invalid_AantalWooneenheden_Throws()
        {
            Assert.Throws<InnovatiefWonenProjectException>(() => new InnovatiefWonenProject(-1, new List<string>(), false, false, 0f, false, false));
        }

        [Fact]
        public void Invalid_Innovatiescore_Throws()
        {
            Assert.Throws<InnovatiefWonenProjectException>(() => new InnovatiefWonenProject(1, new List<string>(), false, false, -1f, false, false));
        }
    }
}