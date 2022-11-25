using AGTIV.Framework.MVC.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class WorkflowMatrixStage : Entity
    {
        [DataMember]
        public string InternalStageName { get; set; }

        [DataMember]
        [ForeignKey("WorkflowMatrix")]
        public Guid MatrixId { get; set; }

        [DataMember]
        public int StageOrder { get; set; }

        [DataMember]
        public bool CCOnly { get; set; }

        [DataMember]
        public bool IsOriginatorActioner { get; set; }

        [DataMember]
        public WorkflowMatrix WorkflowMatrix { get; set; }

        [DataMember]
        public ICollection<WorkflowStageUser> WorkflowStageUser { get; set; }

        [DataMember]
        public ICollection<WorkflowStageGroup> WorkflowStageGroup { get; set; }
    }
}
