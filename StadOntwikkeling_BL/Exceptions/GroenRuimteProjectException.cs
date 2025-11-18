using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class GroenRuimteProjectException : Exception
	{
		public GroenRuimteProjectException()
		{
		}

		public GroenRuimteProjectException(string? message) : base(message)
		{
		}

		public GroenRuimteProjectException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
