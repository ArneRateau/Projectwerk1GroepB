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
        public void MaakGebruiker(string email, bool isAdmin, bool isPartner)
        {
            // TODO hier komt logica om een gebruiker te maken in de db
            throw new NotImplementedException();
        }
    }
}
