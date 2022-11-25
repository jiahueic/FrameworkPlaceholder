using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowMatrixVM
    {
        public Guid Id { get; set; }

        public string MatrixName { get; set; }

        public List<WorkflowMatrixStageVM> WorkflowStage { get; set; }
    }
}
