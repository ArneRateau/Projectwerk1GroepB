using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects();
        List<Project> GetProjectsLite();
        void MaakProject(string titel, int status, DateTime datum, string wijk, string straat, string gemeente, string postcode, string huisnummer, string beschrijving);
    }
}
