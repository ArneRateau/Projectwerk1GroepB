using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IGebruikerManager
    {
        public int MaakGebruiker(string naam, string email, bool isAdmin, bool isPartner);
        public Gebruiker? ZoekGebruikerMetEmail(string email);

        public bool IsGeldigEmail(string email); 

    }
}
