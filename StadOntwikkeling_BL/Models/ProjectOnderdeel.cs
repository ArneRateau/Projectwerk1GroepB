using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public class ProjectOnderdeel
	{
		private int _projectOnderdeelId;
		public int ProjectOnderdeelId
		{
			get { return _projectOnderdeelId; }
			set
			{
				if (value <= 0)
					throw new ProjectOnderdeelException("ProjectOnderdeelId mag niet nul of negatief zijn");
				_projectOnderdeelId = value;
			}
		}
	}
}
