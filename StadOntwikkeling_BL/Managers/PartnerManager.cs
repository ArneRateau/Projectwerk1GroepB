using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class PartnerManager : IPartnerManager
    {
        private IPartnerRepository _repo;

        public PartnerManager(IPartnerRepository repo)
        {
            _repo = repo;
        }
        public List<Partner> GetPartners()
        {
            return _repo.GetPartners();
        }
        public void MakeNewPartner(Partner partner)
        {
            _repo.MakeNewPartner(partner);
        }
        public List<Partner> GetAllPartners()
        {
            return _repo.GetAllPartners();
        }

        public List<Partner> GetPartnersByProjectId(int projectId)
        {
            return _repo.GetPartnersByProjectId(projectId);
        }
    }
}
