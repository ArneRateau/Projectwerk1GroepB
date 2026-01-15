using Xunit;
using StadOntwikkeling_BL.Models;

namespace StadOntwikkeling_Tests.Models
{
    public class FaciliteitTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var f = new Faciliteit(123, "Picknickweide");
            Assert.Equal(123, f.ProjectId);
            Assert.Equal("Picknickweide", f.Naam);
            Assert.Equal(0, f.FaciliteitId); // default value
        }

        [Fact]
        public void Naam_CanBeChanged()
        {
            var f = new Faciliteit(1, "Oud");
            f.Naam = "Nieuw";
            Assert.Equal("Nieuw", f.Naam);
        }
    }
}