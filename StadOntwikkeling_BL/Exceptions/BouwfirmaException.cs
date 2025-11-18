using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Exceptions
{
	public class BouwfirmaException : Exception
	{
		public BouwfirmaException()
		{
		}

		public BouwfirmaException(string? message) : base(message)
		{
		}

		public BouwfirmaException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
