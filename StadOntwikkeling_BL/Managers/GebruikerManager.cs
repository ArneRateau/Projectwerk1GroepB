using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class GebruikerManager : IGebruikerManager
    {
        private readonly IGebruikerRepository _gebruikerRepo;

        public GebruikerManager(IGebruikerRepository gebruikerRepo)
        {
            _gebruikerRepo = gebruikerRepo;
        }

        public int MaakGebruiker(string naam, string email, bool isAdmin, bool isPartner)
        { 

            int newId = _gebruikerRepo.MaakGebruiker(naam, email, isAdmin, isPartner);
            return newId;
        }

		public Gebruiker? ZoekGebruikerMetEmail(string email)
		{
            return _gebruikerRepo.ZoekGebruikerMetEmail(email);
		}

        public bool IsGeldigEmail(string email)
        {
            if (!email.Contains("@"))
            {
                return false;
            }
            else return true; 
        }
    }
}
