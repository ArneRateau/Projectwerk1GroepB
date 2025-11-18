using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class ProjectOnderdeelException : Exception
	{
		public ProjectOnderdeelException()
		{
		}

		public ProjectOnderdeelException(string? message) : base(message)
		{
		}

		public ProjectOnderdeelException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
