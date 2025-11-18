using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StadOntwikkeling_DL.Repos
{
    public class GebruikerRepo : IGebruikerRepo
    {
        public void MaakGebruiker(string email, bool isAdmin, bool isPartner)
        {
            // TODO hier komt logica om gebruiker aan te maken in de database
            throw new NotImplementedException();
        }
    }
}
