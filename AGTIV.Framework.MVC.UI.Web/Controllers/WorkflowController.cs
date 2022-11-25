using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.Workflow;
using AGTIV.Framework.MVC.UI.Web.Attribute;
using AGTIV.Framework.MVC.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    public class WorkflowController : Controller
    {
        private IWorkflowProcess _workflowProcess;
        private IUserProcess _userProcess;
        private IGroupProcess _groupProcess;

        public WorkflowController(
            IWorkflowProcess workflowProcess,
            IUserProcess userProcess,
            IGroupProcess groupProcess
            )
        {
            _workflowProcess = workflowProcess;
            _userProcess = userProcess;
            _groupProcess = groupProcess;
        }

        public ActionResult Detail(int processId)
        {
            var workflowProcess = _workflowProcess.GetProcess(processId);

            if(workflowProcess.CompletionDate == null)
                ViewBag.IsIncomplete = true;
            else
                ViewBag.IsIncomplete = false;

            return View(processId);
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult ConfigurationList()
        {
            var model = _workflowProcess.GetWorkflowConfigurations();

            return View(model);
        }

        [HttpGet]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult ConfigurationEdit(int stepId, int actionId)
        {
            var model = _workflowProcess.GetWorkflowConfiguration(stepId, actionId);

            return View(model);
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult ConfigurationEdit(WorkflowConfigurationVM model)
        {
            if (ModelState.IsValid)
            {
                _workflowProcess.UpdateWorkflowConfiguration(model);
                return RedirectToAction("ConfigurationList");
            }

            return View(model);
        }

        [HttpGet]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult MatrixList()
        {
            var model = _workflowProcess.GetWorkflowConfigurations();

            return View(model);
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult _MatrixList()
        {
            var model = _workflowProcess.GetWorkflowMatrices();
            var viewModel = new List<WorkflowMatrixGridVM>();
            if (model != null && model.Count() > 0)
            {
                foreach(var matrix in model)
                {
                    var matrixVM = new WorkflowMatrixGridVM()
                    {
                        Id = matrix.Id,
                        MatrixName = matrix.MatrixName,
                        Stage1Approvers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage1, matrix),
                        Stage2Approvers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage2, matrix),
                        Stage3Approvers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage3, matrix),
                        Stage4Approvers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage4, matrix),
                        Stage5Approvers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage5, matrix),
                        StageApprovedRecipient = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Approved, matrix),
                        StageRejectedApprovers = GetUserGroupCombined(ConstantHelper.Workflow.d_tblStep.InternalStepName.Rejected, matrix),
                    };

                    viewModel.Add(matrixVM);
                }
            }

            return Json(new { result = viewModel, count = viewModel.Count() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult MatrixCreateEdit(Guid? id)
        {
            WorkflowMatrixVM model = new WorkflowMatrixVM();
            var stage1 = new WorkflowMatrixStageVM();
            var stage2 = new WorkflowMatrixStageVM();
            var stage3 = new WorkflowMatrixStageVM();
            var stage4 = new WorkflowMatrixStageVM();
            var stage5 = new WorkflowMatrixStageVM();
            var stageApproved = new WorkflowMatrixStageVM();
            var stageRejected = new WorkflowMatrixStageVM();

            if (id.HasValue)
            {
                model = _workflowProcess.GetWorkflowMatrix(id.Value);
                stage1 = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage1, model);
                stage2 = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage2, model);
                stage3 = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage3, model);
                stage4 = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage4, model);
                stage5 = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage5, model);
                stageApproved = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Approved, model);
                stageRejected = GetMatrixStageFromMatrix(ConstantHelper.Workflow.d_tblStep.InternalStepName.Rejected, model);
            }

            var viewModel = new WorkflowMatrixCreateEditVM()
            {
                Id = model.Id,
                MatrixName = model.MatrixName,
                Stage1Id = stage1.Id,
                Stage1Approvers = JoinUserWGroup(stage1),
                Stage2Id = stage2.Id,
                Stage2Approvers = JoinUserWGroup(stage2),
                Stage3Id = stage3.Id,
                Stage3Approvers = JoinUserWGroup(stage3),
                Stage4Id = stage4.Id,
                Stage4Approvers = JoinUserWGroup(stage4),
                Stage5Id = stage5.Id,
                Stage5Approvers = JoinUserWGroup(stage5),
                StageApprovedId = stageApproved.Id,
                StageApprovedRecipient = JoinUserWGroup(stageApproved),
                StageRejectedId = stageRejected.Id,
                StageRejectedApprovers = JoinUserWGroup(stageRejected),
            };

            viewModel.UserList = _userProcess.Get()?.OrderBy(x => x.FullName).ToList();
            viewModel.GroupList = _groupProcess.GetAll()?.OrderBy(x => x.Name).ToList();

            return View(viewModel);
        }

        private Guid[] JoinUserWGroup(WorkflowMatrixStageVM stage)
        {
            var users = stage?.WorkflowStageUser?.Select(x => x.User.Id).ToArray() ?? new Guid[] { };
            var groups = stage?.WorkflowStageGroup?.Select(x => x.Group.Id).ToArray() ?? new Guid[] { };
            var userNGroup = new List<Guid>();
            userNGroup.AddRange(users);
            userNGroup.AddRange(groups);

            return userNGroup.ToArray();
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult MatrixCreateEdit(WorkflowMatrixCreateEditVM model)
        {
            if (ModelState.IsValid)
            {
                WorkflowMatrixVM matrix = new WorkflowMatrixVM();
                matrix.Id = model.Id;
                matrix.MatrixName = model.MatrixName;
                matrix.WorkflowStage = new List<WorkflowMatrixStageVM>();

                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage1, model, model.Stage1Id, 1, false, false, model.Stage1Approvers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage2, model, model.Stage2Id, 2, false, false, model.Stage2Approvers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage3, model, model.Stage3Id, 3, false, false, model.Stage3Approvers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage4, model, model.Stage4Id, 4, false, false, model.Stage4Approvers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Stage5, model, model.Stage5Id, 5, false, false, model.Stage5Approvers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Rejected, model, model.StageRejectedId, 6, true, true, model.StageRejectedApprovers));
                matrix.WorkflowStage.Add(CreateStageData(ConstantHelper.Workflow.d_tblStep.InternalStepName.Approved, model, model.StageApprovedId, 7, true, false, model.StageApprovedRecipient));

                var matrixId = _workflowProcess.CreateEditWorkflowMatrix(matrix);

                return RedirectToAction("MatrixCreateEdit", new { id = matrixId });
            }

            ModelState.Clear();
            return View(model);
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult DeleteMatrix(GridVM<WorkflowMatrixGridVM> value)
        {
            _workflowProcess.DeleteWorkflowMatrix(new Guid(value.key.ToString()));
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _WorkflowHistory(int processId)
        {
            var workflowHistories = _workflowProcess.GetWorkflowHistories(processId);
            return Json(new { result = workflowHistories, count = workflowHistories.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _WorkflowLog(int processId)
        {
            var workflowLogs = _workflowProcess.GetWorkflowLogs(processId);
            return Json(new { result = workflowLogs, count = workflowLogs.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult TerminateWorkflow(int processId)
        {
            try
            {
                _workflowProcess.TerminateWorkflow(processId);
                return Json(true);
            }
            catch(Exception)
            {
                return Json(false);
            }
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult _ReassignTask(int taskId)
        {
            var currentUserId = UserAccessControl.GetCurrentUserId();
            var vm = new ReassignTaskVM
            {
                TaskId = taskId,
                UserDdl = _userProcess.Get()
            };
            vm.UserDdl = vm.UserDdl.Where(x => x.Id != currentUserId).ToList();
            return PartialView(vm);
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult _ReassignTask(ReassignTaskVM vm)
        {
            try
            {
                if(vm.AssigneeId == null)
                    throw new Exception("Please select an assignee");

                _workflowProcess.ReassignWorkflow(vm.TaskId, vm.AssigneeId.Value);
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #region Delegation
        public ActionResult Delegation()
        {
            return View();
        }

        public ActionResult _DelegationList()
        {
            var delegations = _workflowProcess.GetMyDelegations();
            return Json(new { result = delegations, count = delegations.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDelegation()
        {
            var vm = new CreateDelegationVM()
            {
                UserDdl = _userProcess.Get(),
            };

            if(!UserAccessControl.MatchAnyRoles(ConstantHelper.Role.Admin))
                vm.DelegateFromId = UserAccessControl.GetCurrentUserId();
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateDelegation(CreateDelegationVM vm)
        {
            _workflowProcess.CreateDelegation(vm);
            return RedirectToAction("Delegation");
        }

        public ActionResult DeleteDelegation(GridVM<DelegationVM> value)
        {
            _workflowProcess.DeleteDelegation((int)value.key);
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private
        private string GetUserGroupCombined(string stageName, WorkflowMatrixVM matrix)
        {
            string combinedStr = string.Empty;

            var stage = matrix.WorkflowStage.Where(y => y.InternalStageName.Equals(stageName)).FirstOrDefault();
            var stageUser = stage?.WorkflowStageUser.Select(z => z.User.FullName);
            var stageGroup = stage?.WorkflowStageGroup.Select(z => z.Group.Name);
            var stageJoin = stageUser?.ToList().Union(stageGroup.Any() ? stageGroup.ToList() : new List<string>());
            combinedStr = string.Join(", ", stageJoin ?? new List<string>());

            return combinedStr;
        }

        private WorkflowMatrixStageVM GetMatrixStageFromMatrix(string stageName, WorkflowMatrixVM matrix)
        {
            var stage = new WorkflowMatrixStageVM();
            stage = matrix.WorkflowStage.Where(x => x.InternalStageName.Equals(stageName)).FirstOrDefault() ?? new WorkflowMatrixStageVM();
            return stage;
        }

        private WorkflowMatrixStageVM CreateStageData(string stageName, WorkflowMatrixCreateEditVM model, Guid stageId, int stageOrder, bool ccOnly, bool isOriginatorActioner, Guid[] approversId)
        {
            return new WorkflowMatrixStageVM()
            {
                Id = stageId,
                MatrixId = model.Id,
                InternalStageName = stageName,
                StageOrder = stageOrder,
                CCOnly = ccOnly,
                IsOriginatorActioner = isOriginatorActioner,
                WorkflowStageUser = approversId?.Where(a => a.ToString().MatchAny(model.UserList.Select(y => y.Id.ToString()).ToArray())).Select(x => new WorkflowStageUserVM() { StageId = stageId, UserId = x }).ToList(),
                WorkflowStageGroup = approversId?.Where(a => a.ToString().MatchAny(model.GroupList.Select(y => y.Id.ToString()).ToArray())).Select(x => new WorkflowStageGroupVM() { StageId = stageId, GroupId = x }).ToList()
            };
        }

        #endregion Private
    }
}