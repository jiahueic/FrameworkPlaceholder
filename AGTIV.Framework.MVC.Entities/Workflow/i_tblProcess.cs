using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class i_tblProcess
    {
        [Key]
        public int ProcessID { get; set; }

        public string KeywordsXML { get; set; }
    }
}
