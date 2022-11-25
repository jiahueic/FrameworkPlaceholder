using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Workflow
{
    public class WorkflowStageUser : Entity
    {
        [DataMember]
        [ForeignKey("WorkflowMatrixStage")]
        public Guid StageId { get; set; }

        [DataMember]
        public WorkflowMatrixStage WorkflowMatrixStage { get; set; }

        [DataMember]
        [ForeignKey("UserProfile")]
        public Guid UserId { get; set; }

        [DataMember]
        public UserProfile UserProfile { get; set; }
    }
}
