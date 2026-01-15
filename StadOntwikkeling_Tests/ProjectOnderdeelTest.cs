using StadOntwikkeling_BL.Exceptions;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_Tests
{
	public class ProjectOnderdeelTest
	{
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_ID(int id)
		{
			var p = new ProjectOnderdeel();
			Assert.Throws<ProjectOnderdeelException>(() => p.ProjectOnderdeelId = id);
		}

		[Fact]
		public void Valid_ID()
		{
			var p = new ProjectOnderdeel();
			p.ProjectOnderdeelId = 10;
			Assert.Equal(10, p.ProjectOnderdeelId);
		}

	}
}
