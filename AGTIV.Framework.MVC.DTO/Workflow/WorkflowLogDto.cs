using System;

namespace AGTIV.Framework.MVC.DTO.Workflow
{
    public class WorkflowLogDto
    {
        public string LogByName { get; set; }

        public string LogType { get; set; }

        public string Description { get; set; }

        public DateTime LogDate { get; set; }
    }
}