using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
    public abstract class ProjectOnderdeel
    {

        private int _projectOnderdeel;
        public int ProjectOnderdeelId
        {
            get { return _projectOnderdeel; }
            set { _projectOnderdeel = value; }
        }
    }
}
