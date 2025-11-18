using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_DL.Repos
{
    public class ProjectRepository : IProjectRepository
    {
        private string connectionString;

        public ProjectRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
