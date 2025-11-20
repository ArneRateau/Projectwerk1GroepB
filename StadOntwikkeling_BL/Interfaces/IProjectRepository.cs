using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();

        List<ProjectDTO> GetProjectsLite();
        void CombineProjectOnderdeel();
        public int MaakProject();
    }
}
