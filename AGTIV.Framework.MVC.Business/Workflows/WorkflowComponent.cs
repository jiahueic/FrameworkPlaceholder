using AGTIV.Framework.General;
using AGTIV.Framework.Workflow.BL;
using AGTIV.Framework.Workflow.BO;
using AGTIV.Framework.MVC.Business.Maintenance;
using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Business.User;
using AGTIV.Framework.MVC.DTO.Workflow;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Transactions;

namespace AGTIV.Framework.MVC.Business.Workflows
{
    public class WorkflowComponent : IWorkflowComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppSetting _appSetting;
        private readonly WorkflowBL _workflowBL;
        private readonly DelegationBL _delegationBL;
        private readonly IUserComponent _userComponent;
        private readonly ICalendarComponent _calendarComponent;

        public WorkflowComponent()
        {
            //For workflow use
            _appSetting = new AppSetting();
            _workflowBL = new WorkflowBL(_appSetting.DBConnString);
        }

        public WorkflowComponent(
            IUnitOfWork unitOfWork,
            IAppSetting appSetting,
            IUserComponent userComponent,
            ICalendarComponent calendarComponent
            )
        {
            _unitOfWork = unitOfWork;
            _appSetting = appSetting;
            _workflowBL = new WorkflowBL(_appSetting.DBConnString);
            _delegationBL = new DelegationBL(_appSetting.DBConnString);
            _userComponent = userComponent;
            _calendarComponent = calendarComponent;
        }

        public List<WorkflowConfiguration> GetConfigurationList()
        {
            List<WorkflowConfiguration> configList = new List<WorkflowConfiguration>();

            var stepList = _unitOfWork.WorkflowRepository.GetEditableStepTemplate();
            var actionList = _unitOfWork.WorkflowRepository.GetEditableActionTemplate();

            configList = (from step in stepList
                          join action in actionList
                          on step.StepID equals action.CurStepID into sa
                          from s in sa.DefaultIfEmpty()
                          orderby step.StepOrder, step.StepID
                          select new WorkflowConfiguration()
                          {
                              StepId = step.StepID,
                              StepName = step.StepName,
                              InternalStepName = step.InternalStepName,
                              ActionId = s != null ? s.ActionID : 0,
                              IsSingleApprover = s != null && s.MinSlot == 1 ? true : false,
                              DueDateDay = step.DueDateDay,
                              EmailSubject = step.EmailNotificationSubject,
                              EmailBody = step.EmailNotificationBody,
                              TaskUrl = step.TaskURL
                          }).ToList();
            
            return configList;
        }

        public WorkflowConfiguration GetConfiguration(int stepId, int actionId)
        {
            var step = _unitOfWork.WorkflowRepository.GetStepTemplateByStepId(stepId);
            var action = _unitOfWork.WorkflowRepository.GetActionTemplateByActionId(actionId);

            var config = new WorkflowConfiguration()
            {
                StepId = step.StepID,
                StepName = step.StepName,
                InternalStepName = step.InternalStepName,
                ActionId = action.ActionID,
                IsSingleApprover = action.MinSlot == 1 ? true : false,
                DueDateDay = step.DueDateDay,
                EmailSubject = step.EmailNotificationSubject,
                EmailBody = step.EmailNotificationBody,
                TaskUrl = step.TaskURL
            };

            return config;
        }

        public void UpdateConfiguration(WorkflowConfiguration configuration)
        {
            var step = _unitOfWork.WorkflowRepository.GetStepTemplateByStepId(configuration.StepId);
            var action = _unitOfWork.WorkflowRepository.GetActionTemplateByActionId(configuration.ActionId);

            step.StepName = configuration.StepName;

            if (!step.InternalStepName.Equals(ConstantHelper.Workflow.d_tblStep.InternalStepName.Start) )
            {
                step.DueDateDay = configuration.DueDateDay;
                step.EmailNotificationSubject = configuration.EmailSubject;
                step.EmailNotificationBody = configuration.EmailBody;
                step.TaskURL = configuration.TaskUrl;

                if(action.ActionID != 0)
                {
                    action.MinSlot = configuration.IsSingleApprover ? 1 : 0;
                }
                else
                {
                    action.MinSlot = -1;
                }
            }
            else
            {
                step.DueDateDay = -1;
                step.EmailNotificationSubject = null;
                step.EmailNotificationBody = null;
                step.TaskURL = null;
                action.MinSlot = -1;
            }

            using (TransactionScope transaction = new TransactionScope())
            {
                _unitOfWork.WorkflowRepository.UpdateStepTemplate(step);

                if(action.ActionID != 0)
                    _unitOfWork.WorkflowRepository.UpdateActionTemplate(action);

                transaction.Complete();
            }
        }

        public List<WorkflowMatrix> GetMatrixList()
        {
            return _unitOfWork.Repository.GetAll<WorkflowMatrix>(
                p => p.WorkflowStage.Select(y => y.WorkflowStageUser.Select(z => z.UserProfile)),
                p => p.WorkflowStage.Select(y => y.WorkflowStageGroup.Select(z => z.Group))
            );
        }

        public WorkflowMatrix GetMatrix(Guid id)
        {
            return _unitOfWork.Repository.Get<WorkflowMatrix>(
                x => x.Id == id,
                p => p.WorkflowStage.Select(y => y.WorkflowStageUser.Select(z => z.UserProfile)),
                p => p.WorkflowStage.Select(y => y.WorkflowStageGroup.Select(z => z.Group))
            )?.FirstOrDefault();
        }

        public Guid CreateEditMatrix(WorkflowMatrix matrix)
        {
            if (matrix != null && matrix.WorkflowStage != null && matrix.WorkflowStage.Count() > 0)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    //Add Matrix if new
                    var existingMatrix = _unitOfWork.Repository.GetByID<WorkflowMatrix>(matrix.Id);
                    if (existingMatrix != null)
                    {
                        existingMatrix.MatrixName = matrix.MatrixName;
                        _unitOfWork.Repository.Update(existingMatrix);

                        foreach (var stage in matrix.WorkflowStage)
                        {
                            var existingStage = _unitOfWork.Repository.GetByID<WorkflowMatrixStage>(stage.Id);
                            if (existingStage != null)
                            {
                                existingStage.InternalStageName = stage.InternalStageName;
                                existingStage.StageOrder = stage.StageOrder;
                                existingStage.CCOnly = stage.CCOnly;
                                existingStage.IsOriginatorActioner = stage.IsOriginatorActioner;
                                _unitOfWork.Repository.Update(existingStage);

                                var existingUserStage = _unitOfWork.Repository.Get<WorkflowStageUser>(x => x.StageId == existingStage.Id);

                                //Delete User
                                var toDeleteStageUser = existingUserStage.Where(x => !stage.WorkflowStageUser.Where(y => x.StageId == y.StageId && x.UserId == y.UserId).Any());
                                foreach (var toDelete in toDeleteStageUser)
                                    _unitOfWork.Repository.Delete<WorkflowStageUser>(toDelete.Id);

                                //Add User
                                var toAddStageUser = stage.WorkflowStageUser.Where(x => !existingUserStage.Where(y => x.StageId == y.StageId && x.UserId == y.UserId).Any());
                                foreach (var toAdd in toAddStageUser)
                                {
                                    toAdd.Status = 1;
                                    _unitOfWork.Repository.Insert<WorkflowStageUser>(toAdd);
                                }

                                var existingUserGroup = _unitOfWork.Repository.Get<WorkflowStageGroup>(x => x.StageId == existingStage.Id);

                                //Delete Group User
                                var toDeleteStageGroup = existingUserGroup.Where(x => !stage.WorkflowStageGroup.Where(y => x.StageId == y.StageId && x.GroupId == y.GroupId).Any());
                                foreach (var toDelete in toDeleteStageGroup)
                                    _unitOfWork.Repository.Delete<WorkflowStageGroup>(toDelete.Id);

                                //Add Group User
                                var toAddStageGroup = stage.WorkflowStageGroup.Where(x => !existingUserGroup.Where(y => x.StageId == y.StageId && x.GroupId == y.GroupId).Any());
                                foreach (var toAdd in toAddStageGroup)
                                {
                                    toAdd.Status = 1;
                                    _unitOfWork.Repository.Insert<WorkflowStageGroup>(toAdd);
                                }
                            }
                            else
                            {
                                stage.Status = 1;
                                _unitOfWork.Repository.Insert(stage);
                            }

                            _unitOfWork.Save();
                        }
                    }
                    else
                    {
                        matrix.Status = 1;
                        _unitOfWork.Repository.Insert(matrix);
                    }

                    _unitOfWork.Save();
                    trans.Complete();
                }
            }

            return matrix.Id;
        }

        public void DeleteMatrix(Guid id)
        {
            var matrix = _unitOfWork.Repository.Get<WorkflowMatrix>(
                x => x.Id == id,
                p => p.WorkflowStage.Select(y => y.WorkflowStageUser),
                p => p.WorkflowStage.Select(y => y.WorkflowStageGroup)
            )?.FirstOrDefault();

            if(matrix != null)
            {
                _unitOfWork.Repository.Delete<WorkflowMatrix>(matrix);
                _unitOfWork.Save();
            }
            
        }

        #region Workflow Detail
        public int StartWorkflow(string matrixName, string referenceNo, Guid requestorId, Dictionary<string, string> additionalKey = null)
        {
            if (string.IsNullOrEmpty(_appSetting.AG_EmailPort) || string.IsNullOrEmpty(_appSetting.AG_EmailHost) || string.IsNullOrEmpty(_appSetting.AG_EmailFrom))
            {
                throw new ArgumentNullException("SMTP host name, SMTP port number and Email address sender in webconfig value cannot be null");
            }

            if (string.IsNullOrEmpty(matrixName) || string.IsNullOrEmpty(referenceNo))
            {
                throw new ArgumentNullException("Matrix Name and Reference No must be provided");
            }

            var matrix = _unitOfWork.Repository.Get<WorkflowMatrix>(
                x => x.MatrixName == matrixName,
                p => p.WorkflowStage.Select(y => y.WorkflowStageUser.Select(z => z.UserProfile)),
                p => p.WorkflowStage.Select(y => y.WorkflowStageGroup.Select(z => z.Group.UserProfiles))
            )?.FirstOrDefault();

            int processId = 0;

            var currentUser = CurrentUser();

            if (matrix != null && matrix.WorkflowStage != null && matrix.WorkflowStage.Count() > 0)
            {
                var wflStage = matrix.WorkflowStage.OrderBy(x => x.StageOrder).ToList();
                var wflConfig = GetConfigurationList();

                StageActioners stageActioners = null;


                for (int i = 0; i < matrix.WorkflowStage.Count(); i++)
                {
                    var stage = wflStage[i];
                    var dueDays = wflConfig.Where(x => x.InternalStepName == wflStage[i].InternalStageName).FirstOrDefault()?.DueDateDay;
                    int newDueDays = _calendarComponent.GetDueDaysBasedOnHolidays(dueDays ?? 0, DateTime.Today, requestorId);

                    var users = new List<UserProfile>();
                    var stageUsers = stage.WorkflowStageUser.Select(x => x.UserProfile);
                    users.AddRange(stageUsers);

                    var stageGroup = stage.WorkflowStageGroup.Select(x => x.Group.UserProfiles);
                    stageGroup.ToList().ForEach(x =>
                    {
                        users.AddRange(x);
                    });

                    var distinctUsers = users.DistinctBy(x => x.Id); //Remove duplicate users
                    var actioners = ConstructActioner(distinctUsers);

                    if (i == 0)
                    {
                        stageActioners = new StageActioners(stage.InternalStageName, newDueDays);

                        if (actioners != null && actioners.Count() > 0)
                            AddActioner(ref stageActioners, actioners);
                    }
                    else
                    {

                        if (!stage.CCOnly)
                        {
                            stageActioners.NextStage(stage.InternalStageName, newDueDays);

                            if (actioners != null && actioners.Count() > 0)
                                AddActioner(ref stageActioners, actioners);
                        }
                        else
                        {
                            MailAddressCollection cc = new MailAddressCollection();
                            users.ForEach(x => cc.Add(new MailAddress(x.EmailAddress)));

                            stageActioners.NextStage(stage.InternalStageName, newDueDays, cc);
                            if (stage.IsOriginatorActioner)
                            {
                                stageActioners.AddActioner(currentUser);
                            }
                        }
                    }
                }

                ProcessKeywords processKeywords = SetWFProcessKeywords(referenceNo, additionalKey);

                processId = _workflowBL.StartWorkflow(ConstantHelper.Workflow.d_tblProcess.ProcessName.Approval, currentUser, processKeywords, stageActioners);
            }

            return processId;
        }

        public void Approve(int processId, int taskId)
        {
            var actioner = CurrentUser();
            if (_workflowBL.CheckIsMyTask(taskId, actioner))
                _workflowBL.ActionTask(taskId, processId, ConstantHelper.Workflow.d_tblAction.ActionName.Approve, actioner);
            else
                throw new Exception("The task does not belong to the user");
        }

        public void Reject(int processId, int taskId)
        {
            var actioner = CurrentUser();
            if (_workflowBL.CheckIsMyTask(taskId, actioner))
                _workflowBL.ActionTask(taskId, processId, ConstantHelper.Workflow.d_tblAction.ActionName.Reject, actioner);
            else
                throw new Exception("The task does not belong to the user");
        }

        public void Resubmit(int processId, int taskId)
        {
            var actioner = CurrentUser();
            if (_workflowBL.CheckIsMyTask(taskId, actioner))
                _workflowBL.ActionTask(taskId, processId, ConstantHelper.Workflow.d_tblAction.ActionName.Resubmit, actioner);
            else
                throw new Exception("The task does not belong to the user");
        }

        public List<WorkflowHistory> GetWorkflowHistories(int processId)
        {
            var dataTable = _workflowBL.GetWorkflowHistory(processId);

            var workflowHistories = (from row in dataTable.AsEnumerable()
                                     select new WorkflowHistory()
                                     {
                                         ActionName = Convert.ToString(row["ActionName"]),
                                         ActionedByName = Convert.ToString(row["ActionedByName"]),
                                         AssigneeName = Convert.ToString(row["AssigneeName"]),
                                         Comments = Convert.ToString(row["Comments"]),
                                         Status = Convert.ToString(row["Status"]),
                                         StepName = Convert.ToString(row["StepName"]),
                                         TaskId = Convert.ToInt32(row["TaskID"]),
                                         ActionedDate = row["ActionedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ActionedDate"]),
                                         AssignedDate = Convert.ToDateTime(row["AssignedDate"]),
                                         DueDate = Convert.ToDateTime(row["DueDate"]),
                                     }).ToList();

            return workflowHistories;
        }

        public List<WorkflowLogDto> GetWorkflowLogs(int processId)
        {
            var dataTable = _workflowBL.GetWorkflowLog(processId);

            var workflowLogs = (from row in dataTable.AsEnumerable()
                                select new WorkflowLogDto()
                                {
                                    LogByName = Convert.ToString(row["LogByName"]),
                                    LogType = Convert.ToString(row["LogType"]),
                                    Description = Convert.ToString(row["LogDescription"]),
                                    LogDate = Convert.ToDateTime(row["LogDate"]),
                                }).ToList();

            return workflowLogs;
        }

        public WorkflowProcessDto GetProcess(int processId)
        {
            var data = _workflowBL.GetProcessInstance(processId);

            var process = new WorkflowProcessDto
            {
                Id = processId,
                CompletionDate = data.CompletionDate == DateTime.MinValue ? (DateTime?)null : data.CompletionDate
            };

            return process;
        }

        public void TerminateWorkflow(int processId)
        {
            _workflowBL.TerminateWorkflow(processId, CurrentUser());
        }

        public void ReassignTask(int taskId, Guid assigneeId)
        {
            var userProfile = _userComponent.Get(assigneeId);
            _workflowBL.ReassignTaskForAdmin(taskId, CurrentUser(), ConstructActioner(userProfile));
        }

        #endregion

        #region Delegation

        public void CreateDelegation(CreateDelegation delegation)
        {
            var delegationFrom = _userComponent.Get(delegation.DelegateFromId);
            var delegationTo = _userComponent.Get(delegation.DelegateToId);

            var delegationInstance = new DelegationInstance
            {
                ProcessTemplateID = 1,
                ProcessName = "Approval",
                ApplicationName = string.Empty,
                DelegationFrom = delegationFrom.Id.ToString(),
                DelegationFromFriendlyName = delegationFrom.FullName,
                DelegationFromEmail = delegationFrom.EmailAddress,
                DelegationTo = delegationTo.Id.ToString(),
                DelegationToFriendlyName = delegationTo.FullName,
                DelegationToEmail = delegationTo.EmailAddress,
                DelegationStartDate = delegation.StartDate,
                DelegationEndDate = delegation.EndDate,
                Active = true
            };

            _delegationBL.InsertDelegationInstance(delegationInstance, CurrentUser().Name);
        }

        public void DeleteDelegation(int delegationId)
        {
            _delegationBL.DeleteDelegationInstance(delegationId, CurrentUser().Name);
        }

        public List<Delegation> GetMyDelegations()
        {
            var delegations = new List<Delegation>();
            var result = _delegationBL.GetMyDelegations(CurrentUser());

            foreach(var item in result)
            {
                delegations.Add(new Delegation
                {
                    DelegationID = item.DelegationID,
                    DelegationFrom = item.DelegationFromFriendlyName,
                    DelegationTo = item.DelegationToFriendlyName,
                    StartDate = item.DelegationStartDate,
                    EndDate = item.DelegationEndDate
                });
            }

            return delegations;
        }
        #endregion

        #region Private
        private Actioner CurrentUser()
        {
            var currentUserId = UserAccessControl.GetCurrentUserId();

            var user = _unitOfWork.Repository.GetByID<UserProfile>(currentUserId);
            if (user == null)
            {
                throw new Exception($"User with id {currentUserId} is not found.");
            }

            var actioner = ConstructActioner(user);

            return actioner;
        }

        private ProcessKeywords SetWFProcessKeywords(string referenceKey, Dictionary<string, string> additionalKey = null)
        {
            ProcessKeywords keywords = new ProcessKeywords(referenceKey, _appSetting.WebUrl.Replace("http://", string.Empty).Replace("https://", string.Empty));

            if(additionalKey != null && additionalKey.Count() > 0)
            {
                foreach(var key in additionalKey)
                {
                    keywords.AddKeywordValue(key.Key, key.Value);
                }
            }

            return keywords;
        }

        private List<Actioner> ConstructActioner(IEnumerable<UserProfile> listUser)
        {
            List<Actioner> listActioner = new List<Actioner>();
            foreach (var user in listUser)
            {
                var actioner = ConstructActioner(user);
                listActioner.Add(actioner);
            }

            return listActioner;
        }

        private Actioner ConstructActioner(UserProfile user)
        {
            Actioner actioner = new Actioner(user.Id.ToString(), user.FullName, user.EmailAddress);
            return actioner;
        }

        private void AddActioner(ref StageActioners stageActioners, IEnumerable<Actioner> listActioner)
        {
            foreach (var actioner in listActioner)
            {
                stageActioners.AddActioner(actioner);
            }
        }

        #endregion Private

        #region Workflow Assembly Call
        public string UpdateWFStage(string referenceKey, int processId, string keywordsXML)
        {
            //var xml = ConversionHelper.XMLDeserialize<ArrayOfKeyValueOfstringstring>(keywordsXML);
            //var department = xml.KeyValueOfstringstring.Where(x => x.Key.Equals("{Department}"))?.FirstOrDefault().Value;
            //var module = xml.KeyValueOfstringstring.Where(x => x.Key.Equals("{Module}"))?.FirstOrDefault().Value;

            //var currentStage = _workflowBL.GetCurrentStepName(processId);
            //var currentInternalStage = _workflowBL.GetCurrentStepInternalName(processId);

            //SQLHelper _sqlHelper = new SQLHelper(ConfigurationManager.ConnectionStrings[ConstantHelper.ConnString.Default].ToString());
            //List<string> field2Update = new List<string>();
            //List<object> fieldValue = new List<object>();

            return "Default";
        }
        #endregion Workflow Assembly Call
    }
}