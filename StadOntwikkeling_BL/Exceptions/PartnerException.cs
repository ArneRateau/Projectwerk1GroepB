using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class PartnerException : Exception
	{
		public PartnerException()
		{
		}

		public PartnerException(string? message) : base(message)
		{
		}

		public PartnerException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
