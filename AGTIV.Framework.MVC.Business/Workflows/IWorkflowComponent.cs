using AGTIV.Framework.MVC.DTO.Workflow;
using AGTIV.Framework.MVC.Entities.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Workflows
{
    public interface IWorkflowComponent
    {
        #region Workflow Configuration
        List<WorkflowConfiguration> GetConfigurationList();

        WorkflowConfiguration GetConfiguration(int stepId, int actionId);

        void UpdateConfiguration(WorkflowConfiguration matrix);
        #endregion Workflow Configuration

        #region Workflow Matrix
        List<WorkflowMatrix> GetMatrixList();

        WorkflowMatrix GetMatrix(Guid id);

        Guid CreateEditMatrix(WorkflowMatrix matrix);

        void DeleteMatrix(Guid id);
        #endregion Workflow Matrix

        #region Workflow Detail
        int StartWorkflow(string matrixName, string referenceNo, Guid requestorId, Dictionary<string, string> additionalKey = null);

        void Approve(int processId, int taskId);

        void Reject(int processId, int taskId);

        void Resubmit(int processId, int taskId);

        List<WorkflowHistory> GetWorkflowHistories(int processId);

        List<WorkflowLogDto> GetWorkflowLogs(int processId);

        WorkflowProcessDto GetProcess(int processId);

        void TerminateWorkflow(int processId);

        void ReassignTask(int processId, Guid assigneeId);

        #endregion

        #region Delegation

        void CreateDelegation(CreateDelegation delegation);

        void DeleteDelegation(int delegationId);

        List<Delegation> GetMyDelegations();

        #endregion

    }
}
