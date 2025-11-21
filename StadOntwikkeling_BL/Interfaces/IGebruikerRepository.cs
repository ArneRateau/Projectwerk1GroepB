using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
	public interface IGebruikerRepository
	{
		int MaakGebruiker(string naam, string email, bool isAdmin, bool isPartner);
		Gebruiker? ZoekGebruikerMetEmail(string email);
	}
}
