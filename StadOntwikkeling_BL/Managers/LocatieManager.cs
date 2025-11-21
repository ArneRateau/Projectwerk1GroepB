using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Managers
{
    public class LocatieManager : ILocatieManager
    {
        private readonly ILocatieRepository _locatieRepository;
        public LocatieManager(ILocatieRepository locatieRepository)
        {
            _locatieRepository = locatieRepository;
        }
        public void updateLocatie(Locatie locatie)
        {
            _locatieRepository.updateLocatie(locatie);
        }
    }
}
