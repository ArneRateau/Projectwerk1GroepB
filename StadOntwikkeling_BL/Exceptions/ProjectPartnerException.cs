using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class ProjectPartnerException : Exception
	{
		public ProjectPartnerException()
		{
		}

		public ProjectPartnerException(string? message) : base(message)
		{
		}

		public ProjectPartnerException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
