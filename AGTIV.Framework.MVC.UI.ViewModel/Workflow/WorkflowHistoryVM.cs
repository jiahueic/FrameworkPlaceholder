using System;
using System.ComponentModel;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowHistoryVM
    {
        [DisplayName("Action")]
        public string ActionName { get; set; }

        [DisplayName("Actioned By")]
        public string ActionedByName { get; set; }

        [DisplayName("Assignee Name")]
        public string AssigneeName { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }

        [DisplayName("Workflow Stage")]
        public string StepName { get; set; }

        public int TaskId { get; set; }

        [DisplayName("Actioned Date")]
        public DateTime? ActionedDate { get; set; }

        [DisplayName("Assigned Date")]
        public DateTime AssignedDate { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }
    }
}