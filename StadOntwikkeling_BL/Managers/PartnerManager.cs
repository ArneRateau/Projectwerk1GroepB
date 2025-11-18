using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class PartnerManager
    {
        private IPartnerRepository repo;

        public PartnerManager(IPartnerRepository repo)
        {
            this.repo = repo;
        }
        public List<Partner> GetPartners()
        {
            return repo.GetPartners();
        }
        public void MakeNewPartner(Partner partner)
        {
            repo.MakeNewPartner();
        }
    }
}
