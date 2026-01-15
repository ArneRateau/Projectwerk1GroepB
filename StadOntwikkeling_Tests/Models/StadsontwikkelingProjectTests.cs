using System.Collections.Generic;
using Xunit;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Exceptions;

namespace StadOntwikkeling_Tests.Models
{
    public class StadsontwikkelingProjectTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var b1 = new Bouwfirma("BAM", "b@bam.be", "09");
            var b2 = new Bouwfirma("Cordeel", "c@cordeel.be", "09");
            var lijst = new List<Bouwfirma> { b1, b2 };

            var s = new StadsontwikkelingProject(
                bouwfirmas: lijst,
                vergunningStatus: VergunningStatus.Goedgekeurd,
                architecturieeleWaarde: true,
                toegankelijkheid: Toegankelijkheid.Gedeeltelijk,
                bezienswaardigheid: true,
                uitlegbord: false,
                infoWandeling: true
            );

            Assert.Equal(2, s.Bouwfirmas.Count);
            Assert.Contains(b1, s.Bouwfirmas);
            Assert.Equal(VergunningStatus.Goedgekeurd, s.VergunningStatus);
            Assert.True(s.ArchitecturieeleWaarde);
            Assert.Equal(Toegankelijkheid.Gedeeltelijk, s.Toegankelijkheid);
            Assert.True(s.Bezienswaardigheid);
            Assert.False(s.Uitlegbord);
            Assert.True(s.InfoWandeling);
        }

        [Fact]
        public void ProjectOnderdeelId_Invalid_ThrowsAndValidSet()
        {
            var s = new StadsontwikkelingProject(new List<Bouwfirma>(), VergunningStatus.InAanvraag, false, Toegankelijkheid.VolledigOpenbaar, false, false, false);

            Assert.Throws<ProjectOnderdeelException>(() => s.ProjectOnderdeelId = 0);
            Assert.Throws<ProjectOnderdeelException>(() => s.ProjectOnderdeelId = -4);

            s.ProjectOnderdeelId = 10;
            Assert.Equal(10, s.ProjectOnderdeelId);
        }
    }
}