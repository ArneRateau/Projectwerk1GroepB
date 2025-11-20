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
        public int MaakProject(string titel, string status, string datum, string wijk, string straat, string gemeente, string postcode, string huisnummer, string beschrijving)
        {
            int postCode = int.Parse(postcode);
            DateTime tijd = DateTime.Parse(datum);
            int Status;
            if (status == "Planning")
            {
                Status = 0;
            }
            else if (status == "Uitvoering")
            {
                Status = 1;
            }
            else {
                Status = 2;
            }
            int newID = _projectRepo.MaakProjectAlgemeen(titel,Status,tijd,wijk,straat,gemeente,postCode,huisnummer,beschrijving);
            return newID;
           
        }


    }
}
