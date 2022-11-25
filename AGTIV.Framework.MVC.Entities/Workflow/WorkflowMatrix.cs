using AGTIV.Framework.MVC.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class WorkflowMatrix : Entity
    {
        [DataMember]
        public string MatrixName { get; set; }

        [DataMember]
        public ICollection<WorkflowMatrixStage> WorkflowStage { get; set; }
    }
}
