using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowConfigurationVM
    {
        [Display(Name = "Step Id")]
        public int StepId { get; set; }

        [Display(Name = "Matrix Name")]
        public string StepName { get; set; }

        [Display(Name = "Internal Step Name")]
        public string InternalStepName { get; set; }

        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }

        [Display(Name = "Email Body")]
        [AllowHtml]
        public string EmailBody { get; set; }

        [Display(Name = "Task URL")]
        public string TaskUrl { get; set; }

        [Display(Name = "Action Id")]
        public int ActionId { get; set; }

        [Display(Name = "Only 1 Approval")]
        public bool IsSingleApprover { get; set; }

        public string IsSingleApproverStr
        {
            get { return IsSingleApprover ? "Yes" : "No"; }
        }
            
        [Display(Name = "Due Date (Day)")]
        public int DueDateDay { get; set; }
    }
}
