using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IGebruikerManager
    {
        public void MaakGebruiker(string email, bool isAdmin, bool isPartner);
    }
}
