using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IPartnerRepository
    {
        List<Partner> GetPartners();

        List<Partner> GetAllPartners();

        List<Partner> GetPartnersByProjectId(int projectId);

        Partner GetPartnerByEmail(string email);
        public int MaakPartner(string naam, string email);

        void KoppelPartnerAanProject(int projectId, int partnerId, string rol);
        



    }
}
