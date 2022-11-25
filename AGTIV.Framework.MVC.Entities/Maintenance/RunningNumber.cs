using AGTIV.Framework.MVC.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Maintenance
{
    public class RunningNumber : Entity
    {
        public string Format { get; set; }

        public string Group { get; set; }

        public int RunningNo { get; set; }
    }
}
