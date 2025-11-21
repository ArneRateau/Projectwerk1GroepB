using StadOntwikkeling_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models.DTO_s
{
    public class ProjectDTO
    {
        public ProjectDTO(int id, string title, string status, string wijk, DateTime startDate)
        {
            Id = id;
            Title = title;
            Status = status;
            Wijk = wijk;
            StartDatum = startDate;
        }
        public ProjectDTO(int id, string title, string status, string wijk, DateTime startDate, List<string> projectTypes)
        {
            Id = id;
            Title = title;
            Status = status;
            Wijk = wijk;
            StartDatum = startDate;
            ProjectTypes = projectTypes;
        }
        public ProjectDTO(int id, string title, string status, string wijk, DateTime startDate, List<string> projectTypes, List<string> partnerNaams)
        {
            Id = id;
            Title = title;
            Status = status;
            Wijk = wijk;
            StartDatum = startDate;
            ProjectTypes = projectTypes;
            PartnerNamen = partnerNaams;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Wijk { get; set; }
        public DateTime StartDatum { get; set; }
        public List<string> ProjectTypes { get; set; } 
        public List<string> PartnerNamen { get; set; }

        public string PartnerNamenDisplay =>
        PartnerNamen != null && PartnerNamen.Any()
        ? string.Join(", ", PartnerNamen)
        : "-";

        public string ProjectTypesDisplay =>
            ProjectTypes != null && ProjectTypes.Any()
                ? string.Join(", ", ProjectTypes)
                : "-";


    }
}
