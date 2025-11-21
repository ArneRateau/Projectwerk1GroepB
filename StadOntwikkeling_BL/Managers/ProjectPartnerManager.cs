using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class ProjectPartnerManager : IProjectPartnerManager
    {
        private IProjectPartnerRepository _repo;

        public ProjectPartnerManager(IProjectPartnerRepository repo)
        {
            _repo = repo;
        }
        public bool HeeftPartnerRolVoorProject(Project pr, Partner pa, string rol)
        {
            return _repo.HeeftPartnerRolVoorProject(pr, pa, rol);
        }
        public void KoppelPartnerAanProject(Project pr, Partner pa, string rol)
        {
            _repo.KoppelPartnerAanProject(pr, pa, rol);
        }
    }
}
