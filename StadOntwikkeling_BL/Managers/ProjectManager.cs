using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class ProjectManager 
    {
        private IProjectRepository repo;

        public ProjectManager(IProjectRepository repo)
        {
            this.repo = repo;
        }
        public List<Project> GetProjects()
        {
            return repo.GetProjects();
        }
    }
}
