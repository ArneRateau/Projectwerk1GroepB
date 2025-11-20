using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Mapper
{
	public class LocatieMapper
	{
		public static Locatie MapToDomain(LocatieUI l)
		{
			return new Locatie(l.Id, l.Straat, l.Postcode, l.Gemeente, l.Wijk, l.Huisnummer);
		}
		public static LocatieUI MapFromDomain(Locatie l)
		{
			return new LocatieUI(l.Id, l.Straat, l.Postcode, l.Gemeente, l.Wijk, l.Huisnummer);
		}
	}
}
