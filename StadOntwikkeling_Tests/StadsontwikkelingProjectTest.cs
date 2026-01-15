using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class StadsontwikkelingProjectTest
	{
		[Fact]
		public void Constructor_ShouldSetProperties()
		{
			var s = new StadsontwikkelingProject(
				new List<Bouwfirma>(),
				VergunningStatus.Goedgekeurd,
				true,
				Toegankelijkheid.VolledigOpenbaar,
				true,
				true,
				false
			);

			Assert.Equal(VergunningStatus.Goedgekeurd, s.VergunningStatus);
			Assert.True(s.ArchitecturieeleWaarde);
			Assert.True(s.Bezienswaardigheid);
		}

	}
}
