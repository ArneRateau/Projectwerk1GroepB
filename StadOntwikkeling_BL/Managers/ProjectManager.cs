using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class ProjectManager : IProjectManager
    {
        private IProjectRepository _projectRepo;

        public ProjectManager(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }
        public List<Project> GetProjects()
        {
            return _projectRepo.GetProjects();
        }
        public List<ProjectDTO> GetProjectsLite()
        {
            return _projectRepo.GetProjectsLite();
        }
        // nog niet zeker over parameters
        public int MaakProject()
        {
            int newID = _projectRepo.MaakProject();
            return newID;
        }
    }
}
