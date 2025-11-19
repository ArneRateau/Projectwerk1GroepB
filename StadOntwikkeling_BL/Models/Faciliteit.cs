using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
    public class Faciliteit
    {
        public Faciliteit(int projectId, string naam)
        {
            ProjectId = projectId;
            Naam = naam;
        }

        public int FaciliteitId { get; set; }

        public int ProjectId { get; set; }
        public string Naam { get; set; }

    }
}
