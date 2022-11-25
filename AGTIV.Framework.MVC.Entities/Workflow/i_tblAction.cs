using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class i_tblAction
    {
        [Key]
        public int ActionID { get; set; }

        public string ActionName { get; set; }
    }
}
