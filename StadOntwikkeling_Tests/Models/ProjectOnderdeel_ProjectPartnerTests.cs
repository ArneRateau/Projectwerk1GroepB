using System;
using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Enums;

namespace StadOntwikkeling_Tests.Models
{
    public class ProjectOnderdeel_ProjectPartnerTests
    {
        [Fact]
        public void ProjectOnderdeel_SetInvalidId_Throws()
        {
            var onderdeel = new ProjectOnderdeel();
            Assert.Throws<ProjectOnderdeelException>(() => onderdeel.ProjectOnderdeelId = 0);
            Assert.Throws<ProjectOnderdeelException>(() => onderdeel.ProjectOnderdeelId = -5);
        }

        [Fact]
        public void ProjectPartner_CreateValid_SetsProperties()
        {
            var loc = new Locatie("Straat", "9000", "Gent", "Wijk", "1");
            var project = new Project("T", DateTime.Today, StadOntwikkeling_BL.Enums.Status.Planning, "B", loc, new List<ProjectPartner>(), new List<ProjectOnderdeel>());
            var partner = new Partner("Naam", "p@p.com");

            var pp = new ProjectPartner(partner, project, "Projectleider");
            Assert.Equal(partner, pp.Partner);
            Assert.Equal(project, pp.Project);
            Assert.Equal("Projectleider", pp.Rol);
            Assert.Null(pp.Naam);

            var pp2 = new ProjectPartner(5, partner, project, "Communicatie");
            Assert.Equal(5, pp2.Id);
            Assert.Equal("Communicatie", pp2.Rol);
        }

        
    }
}
