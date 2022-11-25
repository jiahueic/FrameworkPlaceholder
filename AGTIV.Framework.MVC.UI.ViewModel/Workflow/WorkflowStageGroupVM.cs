using AGTIV.Framework.MVC.UI.ViewModel.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowStageGroupVM
    {
        public Guid Id { get; set; }

        public Guid StageId { get; set; }

        public Guid GroupId { get; set; }

        public GroupVM Group { get; set; }
    }
}
