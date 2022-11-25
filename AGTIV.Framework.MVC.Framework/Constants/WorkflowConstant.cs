using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class Workflow
        {
            public class d_tblProcess
            {
                public class ProcessName
                {
                    public const string Approval = "Approval";
                }
            }

            public class d_tblStep
            {
                public class InternalStepName
                {
                    public const string Draft = "Draft";
                    public const string Start = "Start";
                    public const string Stage1 = "Stage1";
                    public const string Stage2 = "Stage2";
                    public const string Stage3 = "Stage3";
                    public const string Stage4 = "Stage4";
                    public const string Stage5 = "Stage5";
                    public const string Approved = "Approved";
                    public const string Rejected = "Rejected";
                    public const string Error = "Error";
                    public const string GoTo = "GoTo";
                    public const string Terminated = "Terminated";
                }
            }

            public class d_tblAction
            {
                public class ActionName
                {
                    public const string Approve = "Approve";
                    public const string Reject = "Reject";
                    public const string Resubmit = "Resubmit";
                }
            }

            public class Keyword
            {
                public const string WebUrl = "{WebUrl}";
            }
        }
    }
}
