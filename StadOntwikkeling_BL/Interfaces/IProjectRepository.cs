using StadOntwikkeling_BL.Models;
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

        List<Project> GetProjectsLite();
        void CombineProjectOnderdeel();
    }
}
