using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class StadsontwikkelingProjectException : Exception
	{
		public StadsontwikkelingProjectException()
		{
		}

		public StadsontwikkelingProjectException(string? message) : base(message)
		{
		}

		public StadsontwikkelingProjectException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
