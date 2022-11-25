using AGTIV.Framework.MVC.Entities.Maintenance;
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
    public class WorkflowStageGroup : Entity
    {
        [DataMember]
        [ForeignKey("WorkflowMatrixStage")]
        public Guid StageId { get; set; }

        [DataMember]
        public WorkflowMatrixStage WorkflowMatrixStage { get; set; }

        [DataMember]
        [ForeignKey("Group")]
        public Guid GroupId { get; set; }

        [DataMember]
        public Group Group { get; set; }
    }
}
