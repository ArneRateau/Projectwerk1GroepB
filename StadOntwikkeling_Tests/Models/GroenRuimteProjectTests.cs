using System;
using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class GroenRuimteProjectTests
    {
        [Fact]
        public void Create_Valid_GreenProject()
        {
            var g = new GroenRuimteProject(1000.0, 0.5, 3, new List<string> { "Picknick" }, true, 4.2);
            Assert.Equal(1000.0, g.Oppervlakte);
            Assert.Equal(0.5, g.Biodiversiteitscore);
            Assert.Equal(3, g.AantalWandelpaden);
            Assert.True(g.InWandelroutes);
        }

        [Fact]
        public void Invalid_Oppervlakte_Throws()
        {
            Assert.Throws<GroenRuimteProjectException>(() => new GroenRuimteProject(0, 0.5, 1, new List<string>(), false, 4.0));
        }

        [Fact]
        public void Invalid_Biodiversiteit_Throws()
        {
            Assert.Throws<GroenRuimteProjectException>(() => new GroenRuimteProject(10, -0.1, 1, new List<string>(), false, 4.0));
        }

        [Fact]
        public void Invalid_AantalWandelpaden_Throws()
        {
            Assert.Throws<GroenRuimteProjectException>(() => new GroenRuimteProject(10, 0.1, -1, new List<string>(), false, 4.0));
        }
    }
}