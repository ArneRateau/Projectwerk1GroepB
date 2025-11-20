using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Mapper
{
	public class PartnerMapper
	{
		public static Partner MapToDomain(PartnerUI p)
		{
			throw new NotImplementedException();
            //return new Partner(p.Id, p.Naam, LocatieMapper.MapToDomain(p.Locatie), p.Email);
        }
		public static PartnerUI MapFromDomain(Partner p)
		{
			throw new NotImplementedException();
        }
	}
}
