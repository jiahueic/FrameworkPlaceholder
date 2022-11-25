using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class i_tblTask
    {
        [Key]
        public int TaskID { get; set; }

        public int ActionID { get; set; }

        public int ProcessID { get; set; }

        public int StepTemplateID { get; set; }

        public Guid ActionedBy { get; set; }
    }
}
