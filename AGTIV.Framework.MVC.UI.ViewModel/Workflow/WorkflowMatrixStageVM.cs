using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowMatrixStageVM
    {
        public Guid Id { get; set; }

        public Guid MatrixId { get; set; }

        public string InternalStageName { get; set; }

        public int StageOrder { get; set; }

        public bool CCOnly { get; set; }

        public bool IsOriginatorActioner { get; set; }

        public List<WorkflowStageUserVM> WorkflowStageUser { get; set; }

        public List<WorkflowStageGroupVM> WorkflowStageGroup { get; set; }
    }
}
