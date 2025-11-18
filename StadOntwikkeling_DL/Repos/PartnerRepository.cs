using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_DL.Repos
{
    public class PartnerRepository : IPartnerRepository
    {
        private string connectionString;

        public PartnerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
