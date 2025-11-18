using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_DL.Repos
{
    public class ProjectPartnerRepository : IProjectPartnerRepository
    {
        private string connectionString;

        public ProjectPartnerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ProjectPartner GetPartnerByRol()
        {
            throw new NotImplementedException();
        }
        
    }
}
