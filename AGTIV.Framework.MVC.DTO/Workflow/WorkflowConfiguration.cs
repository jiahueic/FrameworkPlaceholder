using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.DTO.Workflow
{
    [Serializable]
    [DataContract]
    public class WorkflowConfiguration
    {
        [DataMember]
        public int StepId { get; set; }

        [DataMember]
        public string StepName { get; set; }

        [DataMember]
        public string InternalStepName { get; set; }

        [DataMember]
        public string EmailSubject { get; set; }

        [DataMember]
        public string EmailBody { get; set; }

        [DataMember]
        public string TaskUrl { get; set; }

        [DataMember]
        public int ActionId { get; set; }

        [DataMember]
        public bool IsSingleApprover { get; set; }

        [DataMember]
        public int DueDateDay { get; set; }
    }
}
