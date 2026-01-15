using System;
using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Enums;

namespace StadOntwikkeling_Tests.Models
{
    public class ProjectTests
    {
        [Fact]
        public void Create_ValidProject_SetsProperties()
        {
            var loc = new Locatie("Straat", "9000", "Gent", "Wijk", "12");
            var p = new Project("Titel", DateTime.Today, Status.Planning, "Beschrijving", loc, new List<ProjectPartner>(), new List<ProjectOnderdeel>());
            Assert.Equal("Titel", p.Titel);
            Assert.Equal("Beschrijving", p.Beschrijving);
            Assert.Equal(loc, p.Locatie);
            Assert.Equal(Status.Planning, p.Status);
        }

        [Fact]
        public void Create_NullLocatie_Throws()
        {
            Assert.Throws<ProjectException>(() => new Project("T", DateTime.Today, Status.Planning, "B", null!, new List<ProjectPartner>(), new List<ProjectOnderdeel>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_InvalidTitel_Throws(string titel)
        {
            var loc = new Locatie("S", "9000", "G", "W", "1");
            Assert.Throws<ProjectException>(() => new Project(titel!, DateTime.Today, Status.Planning, "B", loc, new List<ProjectPartner>(), new List<ProjectOnderdeel>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_InvalidBeschrijving_Throws(string besch)
        {
            var loc = new Locatie("S", "9000", "G", "W", "1");
            Assert.Throws<ProjectException>(() => new Project("T", DateTime.Today, Status.Planning, besch!, loc, new List<ProjectPartner>(), new List<ProjectOnderdeel>()));
        }
    }
}