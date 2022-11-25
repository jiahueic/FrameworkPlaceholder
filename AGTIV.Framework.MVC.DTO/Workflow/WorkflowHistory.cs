using System;

namespace AGTIV.Framework.MVC.DTO.Workflow
{
    public class WorkflowHistory
    {
        public string ActionName { get; set; }

        public string ActionedByName { get; set; }

        public string AssigneeName { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }

        public string StepName { get; set; }

        public int TaskId { get; set; }

        public DateTime? ActionedDate { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}