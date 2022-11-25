using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    [Table("d_tblAction")]
    public class d_tblAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ActionID { get; set; }

        public string ActionName { get; set; }

        public int CurStepID { get; set; }

        public int NextStepID { get; set; }

        public int MinSlot { get; set; }

        public int ProcessID { get; set; }
    }
}
