using System;
using System.ComponentModel;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowLogVM
    {
        [DisplayName("Log By")]
        public string LogByName { get; set; }

        [DisplayName("Type")]
        public string LogType { get; set; }

        [DisplayName("Message")]
        public string Description { get; set; }

        [DisplayName("Date")]
        public DateTime LogDate { get; set; }
    }
}