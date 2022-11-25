using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class Status
        {
            public const int Inactive = 0;
            public const int Active = 1;
            public const int PendingApproval = 2;
            public const int Approved = 3;
            public const int Rejected = 4;

            public static int GetStatusInt(string stepName)
            {
                switch (stepName)
                {
                    case Workflow.d_tblStep.InternalStepName.Start:
                    case Workflow.d_tblStep.InternalStepName.Stage1:
                    case Workflow.d_tblStep.InternalStepName.Stage2:
                    case Workflow.d_tblStep.InternalStepName.Stage3:
                    case Workflow.d_tblStep.InternalStepName.Stage4:
                    case Workflow.d_tblStep.InternalStepName.Stage5:
                        return PendingApproval;
                    case Workflow.d_tblStep.InternalStepName.Approved:
                        return Approved;
                    case Workflow.d_tblStep.InternalStepName.Rejected:
                        return Rejected;
                    default:
                        return Active;
                }
            }
        }
    }
}
