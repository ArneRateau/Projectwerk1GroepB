using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using System;
using System.Collections.Generic;

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
        public int MaakProject(
            string titel,
            string status,
            string datum,
            string wijk,
            string straat,
            string gemeente,
            string postcode,
            string huisnummer,
            string beschrijving,
            bool bam,
            bool cord,
            bool alhe,
            bool demo,
            string vergunningsStatus,
            bool archWaa,
            string openbareToegang,
            bool bezienWaard,
            bool uitlegBord,
            bool infoWand,
            bool stadsType)
        {
            int postCode = int.Parse(postcode);
            DateTime tijd = DateTime.Parse(datum);

            int Status;
            if (status == "Planning")
                Status = 0;
            else if (status == "Uitvoering")
                Status = 1;
            else
                Status = 2;

            int newID = _projectRepo.MaakProjectAlgemeen(
                titel,
                Status,
                tijd,
                wijk,
                straat,
                gemeente,
                postCode,
                huisnummer,
                beschrijving
            );

            if (stadsType)
            {
                int Vergun, archWaarde = 0, Toegang, Beziens = 0, UitlegBord = 0, InfoWandeling = 0;

                if (vergunningsStatus == "in aanvraag")
                    Vergun = 0;
                else if (vergunningsStatus == "Goedgekeurd")
                    Vergun = 1;
                else
                    Vergun = 2;

                if (archWaa)
                    archWaarde = 1;

                if (openbareToegang == "volledig openbaar")
                    Toegang = 0;
                else if (openbareToegang == "Gedeelterlijk")
                    Toegang = 1;
                else
                    Toegang = 2;

                if (bezienWaard)
                    Beziens = 1;

                if (uitlegBord)
                    UitlegBord = 1;

                if (infoWand)
                    InfoWandeling = 1;

                int stadId = _projectRepo.MaakProjectStads(
                    newID,
                    Vergun,
                    archWaarde,
                    Toegang,
                    Beziens,
                    UitlegBord,
                    InfoWandeling
                );

                List<int> bouwfirmas = new List<int>();
                if (bam) bouwfirmas.Add(1);
                if (cord) bouwfirmas.Add(2);
                if (alhe) bouwfirmas.Add(3);
                if (demo) bouwfirmas.Add(4);

                foreach (int id in bouwfirmas)
                {
                    _projectRepo.AddBouwFirmaAanStads(newID, id);
                }
            }

            return newID;
        }

        public Project GetProjectById(int id)
        {
            return _projectRepo.GetProjectById(id);
        }

        public void updateProject(Project toUpdate)
        {
            _projectRepo.updateProject(toUpdate);
        }

        // Required by interface but not implemented
        void IProjectManager.MaakProject()
        {
            throw new NotImplementedException();
        }
    }
}
