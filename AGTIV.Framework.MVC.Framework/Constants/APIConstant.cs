using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class API
        {
            public class Path
            {
                #region User
                public const string Login = "api/Token";
                public const string ForgotPassword = "api/Password/Forgot";
                public const string ResetPassword = "api/Password/Reset";
                public const string Signup = "api/User/Register";
                public const string ChangePassword = "api/Password/Change";

                public const string User = "api/User";
                public const string UserById = "api/User/{0}";
                public const string UploadProfilePicture = "api/User/Image";
                #endregion

                #region Role
                public const string Role = "api/Role";
                #endregion

                #region Request
                public const string SaveRequest = "api/Request/Save";
                public const string Request = "api/Request";
                public const string Requests = "api/Requests";
                public const string ApprovedRequests = "api/Requests/Approved";
                public const string PendingRequests = "api/Requests/Pending";
                public const string RequestById = "api/Request/{0}";
                public const string RequestByRequestId = "api/Request/RequestId/{0}";
                public const string ApproveITRequest = "api/Request/Approve/{0}/{1}/{2}/{3}";
                public const string TotalSubrequestsBySubcategory = "api/Request/Subrequest/TotalBySubcategory";
                #endregion

                #region Workflow
                public const string GetWorkflowConfigurations = "api/Workflow/ConfigurationList";
                public const string GetWorkflowConfiguration = "api/Workflow/Configuration/{0}/{1}";
                public const string UpdateWorkflowConfiguration = "api/Workflow/Configuration";
                public const string GetWorkflowMatrices = "api/Workflow/MatrixList";
                public const string GetWorkflowMatrix = "api/Workflow/Matrix/{0}";
                public const string CreateEditWorkflowMatrix = "api/Workflow/Matrix";
                public const string DeleteWorkflowMatrix = "api/Workflow/Matrix/{0}";
                public const string WorkflowProcess = "api/Workflow/Process/{0}";
                public const string WorkflowProcessHistories = "api/Workflow/Process/{0}/Histories";
                public const string WorkflowProcessLogs = "api/Workflow/Process/{0}/Logs";
                public const string WorkflowTask = "api/Workflow/Task/{0}";
                public const string WorkflowDelegation = "api/Workflow/Delegation";
                public const string WorkflowDelegations = "api/Workflow/Delegations";
                public const string WorkflowDelegationWithId = "api/Workflow/Delegation/{0}";
                public const string StartWorkflow = "api/Workflow/StartWorkflow/{0}/{1}";
                public const string ApproveRequest = "api/Workflow/Approve/{0}/{1}";
                public const string RejectRequest = "api/Workflow/Approve/{0}/{1}";
                public const string ResubmitRequest = "api/Workflow/Approve/{0}/{1}";
                #endregion

                #region Group

                public const string Group = "api/Group";
                public const string GroupwithId = "api/Group/{0}";
                public const string Groups = "api/Groups";

                #endregion

                #region Calendar Profile

                public const string CalendarProfileList = "api/Calendar/List";
                public const string CalendarProfile = "api/Calendar";
                public const string CalendarParentProfiles = "api/Calendar/ParentProfiles";
                public const string CalendarProfileWithId = "api/Calendar/{0}";
                public const string CalendarProfileHoliday = "api/Calendar/Holiday";
                public const string CalendarProfileHolidayWithId = "api/Calendar/Holiday/{0}";
                public const string CalendarProfileHolidays = "api/Calendar/{0}/Holidays";
                public const string CalendarProfileDelete = "api/Calendar/Delete/{0}";
                public const string CalendarProfileDdl = "api/Calendar/Dropdown";

                #endregion

                #region SubRequestCategory

                public const string SubRequestCategory = "api/SubRequestCategory";
                public const string SubRequestCategoryWithId = "api/SubRequestCategory/{0}";
                public const string SubRequestCategories = "api/SubRequestCategories";

                #endregion

                #region SubRequestSubCategory

                public const string SubRequestSubCategory = "api/SubRequestSubCategory";
                public const string SubRequestSubCategoryWithId = "api/SubRequestSubCategory/{0}";
                public const string SubRequestSubCategories = "api/SubRequestSubCategories";

                #endregion

                #region Elmah
                public const string GetElmah = "api/ElmahLog";
                public const string GetElmahError = "api/ElmahLog/{0}";
                #endregion Elmah

                #region JKR
                public const string JKRRequests = "api/JKR/Request";
                public const string JKRRequestsPendingApproved = "api/JKR/Request/Pending";
                public const string JKRRequestById = "api/JKR/Request/{0}";
                public const string JKRRequestSave = "api/JKR/Request/Save";
                public const string JKRRequestSubmit = "api/JKR/Request/Submit";
                public const string JKRRequestApprove = "api/JKR/Request/Approve";
                public const string JKRRequestReject = "api/JKR/Request/Reject";
                public const string JKRRequestInspectionReport = "api/JKR/Request/Inspection";
                #endregion

                #region Student
                public const string Student = "api/Student";
                public const string StudentWithId = "api/Student/{0}";
                public const string Students = "api/Students";
                #endregion


            }

            public class Parameter
            {
                public class Workflow
                {
                    public const string StepId = "{stepId}";
                    public const string ActionId = "{actionId}";
                }
            }
        }
    }
}
