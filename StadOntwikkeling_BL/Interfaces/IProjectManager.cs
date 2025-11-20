using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StadOntwikkeling_BL.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects();
        List<ProjectDTO> GetProjectsLite();
        public int MaakProject(string titel, string status, string datum, string wijk, string straat, string gemeente, string postcode, string huisnummer, string beschrijving);
        
    }
}
