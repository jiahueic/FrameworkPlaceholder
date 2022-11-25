using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class d_tblStep
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StepID { get; set; }

        public string StepName { get; set; }

        public string InternalStepName { get; set; }

        public int StepOrder { get; set; }

        public int DueDateDay { get; set; }

        public bool EmailNotification { get; set; }

        public bool EmailToAssignee { get; set; }

        public bool EmailToOriginator { get; set; }

        public bool EmailCCOriginator { get; set; }

        public string EmailNotificationSubject { get; set; }

        public string EmailNotificationBody { get; set; }

        public string TaskURL { get; set; }

        public bool LastStep { get; set; }

        public bool EmailOnlyStep { get; set; }

        public bool CodeOnlyStep { get; set; }

        public string AssemblyName { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public int ProcessID { get; set; }
    }
}
