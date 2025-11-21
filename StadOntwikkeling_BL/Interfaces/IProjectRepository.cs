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
		void UpdateProject(Project toUpdate);
        int MaakProjectAlgemeen(string v1, int v2, DateTime now, string v3, string v4, string v5, int v6, string v7, string v8);
        int MaakProjectStads(int newID,int Vergun,int archWaarde,int Toegang,int Beziens,int UitlegBord,int InfoWandeling);
        void AddBouwFirmaAanStads(int newID, int id);
        int maakProjectGroen(int projectId,int oppvlak, double bioSco, int aanWandel, int toeWand, double bezoekScore);
        int AddFaciliteit(int newID, string faciliteit);
        int MaakProjectInno(int newID, int aantalWooneenheden, string woonvormen, int rondL, int showwoning,double ArchInnoScore, int samenErf, int samenToer);
    }
}
