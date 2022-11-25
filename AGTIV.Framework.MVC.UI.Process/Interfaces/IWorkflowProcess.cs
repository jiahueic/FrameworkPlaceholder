using AGTIV.Framework.MVC.UI.ViewModel.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IWorkflowProcess
    {
        List<WorkflowConfigurationVM> GetWorkflowConfigurations();

        WorkflowConfigurationVM GetWorkflowConfiguration(int stepId, int actionId);

        void UpdateWorkflowConfiguration(WorkflowConfigurationVM model);

        List<WorkflowMatrixVM> GetWorkflowMatrices();

        WorkflowMatrixVM GetWorkflowMatrix(Guid id);

        Guid CreateEditWorkflowMatrix(WorkflowMatrixVM model);

        void DeleteWorkflowMatrix(Guid id);

        WorkflowProcessVM GetProcess(int processId);

        List<WorkflowHistoryVM> GetWorkflowHistories(int processId);

        List<WorkflowLogVM> GetWorkflowLogs(int processId);

        void TerminateWorkflow(int processId);

        void ReassignWorkflow(int taskId, Guid assigneeId);

        List<DelegationVM> GetMyDelegations();

        void CreateDelegation(CreateDelegationVM vm);

        void DeleteDelegation(int delegationID);

        int StartWorkflow(string matrixName, string referenceKey);

        void ApproveRequest(int processId, int taskId);

        void RejectRequest(int processId, int taskId);

        void ResubmitRequest(int processId, int taskId);
    }
}
