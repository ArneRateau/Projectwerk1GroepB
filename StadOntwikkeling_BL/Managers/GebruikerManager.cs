using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class GebruikerManager : IGebruikerManager
    {
        private readonly IGebruikerRepo _gebruikerRepo;

        public GebruikerManager(IGebruikerRepo gebruikerRepo)
        {
            _gebruikerRepo = gebruikerRepo;
        }

        

        public void MaakGebruiker(string email, bool isAdmin, bool isPartner)
        { 

            _gebruikerRepo.MaakGebruiker(email, isAdmin, isPartner);
        }
    }
}
