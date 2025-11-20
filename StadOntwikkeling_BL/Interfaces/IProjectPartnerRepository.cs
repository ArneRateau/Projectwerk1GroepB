using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IProjectPartnerRepository
    {
        bool HeeftPartnerRolVoorProject(Project pr, Partner pa, string rol);
        void KoppelPartnerAanProject(Project pr, Partner pa, string rol);
    }
}
