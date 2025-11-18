using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class InnovatiefWonenProjectException : Exception
	{
		public InnovatiefWonenProjectException()
		{
		}

		public InnovatiefWonenProjectException(string? message) : base(message)
		{
		}

		public InnovatiefWonenProjectException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
