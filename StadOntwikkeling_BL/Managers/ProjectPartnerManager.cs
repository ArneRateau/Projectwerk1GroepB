using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class ProjectPartnerManager
    {
        private IProjectPartnerRepository repo;

        public ProjectPartnerManager(IProjectPartnerRepository repo)
        {
            this.repo = repo;
        }
        public ProjectPartner GetPartnerByRol()
        {
            return repo.GetPartnerByRol();
        }
    }
}
