using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IPartnerManager
    {
        List<Partner> GetPartners();
        List<Partner> GetAllPartners();

        List<Partner> GetPartnersByProjectId(int projectId);

        int MaakPartner(string naam, string email);


    }
}
