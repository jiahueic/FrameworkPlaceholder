using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowMatrixGridVM
    {
        public Guid Id { get; set; }

        public string MatrixName { get; set; }

        public string Stage1Approvers { get; set; }

        public string Stage2Approvers { get; set; }

        public string Stage3Approvers { get; set; }

        public string Stage4Approvers { get; set; }

        public string Stage5Approvers { get; set; }

        public string StageApprovedRecipient { get; set; }

        public string StageRejectedApprovers { get; set; }
    }
}
