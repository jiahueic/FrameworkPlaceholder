using AGTIV.Framework.MVC.Business.Workflows;
using AGTIV.Framework.MVC.DTO.Workflow;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [RoutePrefix("api/Workflow")]
    public class WorkflowController : ApiController
    {
        private IWorkflowComponent _workflowComponent { get; set; }
        public WorkflowController(IWorkflowComponent workflowComponent)
        {
            _workflowComponent = workflowComponent;
        }

        [Route("ConfigurationList")]
        [HttpGet]
        public IHttpActionResult GetWorkflowConfigurationList()
        {
            var list = _workflowComponent.GetConfigurationList();
            return Ok(list);
        }

        [Route("Configuration/{stepId}/{actionId}")]
        [HttpGet]
        public IHttpActionResult GetWorkflowConfiguration(int stepId, int actionId)
        {
            var config = _workflowComponent.GetConfiguration(stepId, actionId);
            return Ok(config);
        }

        [Route("Configuration")]
        [HttpPost]
        public IHttpActionResult UpdateWorkflowConfiguration(WorkflowConfiguration config)
        {
            _workflowComponent.UpdateConfiguration(config);
            return Ok();
        }

        [Route("MatrixList")]
        [HttpGet]
        public IHttpActionResult GetWorkflowMatrixList()
        {
            var list = _workflowComponent.GetMatrixList();
            return Ok(list);
        }

        [Route("Matrix/{id}")]
        [HttpGet]
        public IHttpActionResult GetWorkflowMatrix(Guid id)
        {
            var matrix = _workflowComponent.GetMatrix(id);
            return Ok(matrix);
        }

        [Route("Matrix")]
        [HttpPost]
        public IHttpActionResult UpdateWorkflowMatrix(WorkflowMatrix matrix)
        {
            var result = _workflowComponent.CreateEditMatrix(matrix);
            return Ok(result);
        }

        [Route("Matrix/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteWorkflowMatrix(Guid id)
        {
            _workflowComponent.DeleteMatrix(id);
            return Ok();
        }

        #region Workflow Detail

        [Route("Process/{processId}")]
        [HttpGet]
        public IHttpActionResult GetProcess(int processId)
        {
            var result = _workflowComponent.GetProcess(processId);
            return Ok(result);
        }

        [Route("Process/{processId}/Histories")]
        [HttpGet]
        public IHttpActionResult GetWorkflowHistories(int processId)
        {
            var workflowHistories = _workflowComponent.GetWorkflowHistories(processId);
            return Ok(workflowHistories);
        }

        [Route("Process/{processId}/Logs")]
        [HttpGet]
        public IHttpActionResult GetWorkflowLogs(int processId)
        {
            var workflowLogs = _workflowComponent.GetWorkflowLogs(processId);
            return Ok(workflowLogs);
        }

        [Route("Process/{processId}")]
        [HttpDelete]
        public IHttpActionResult TerminateWorkflow(int processId)
        {
            _workflowComponent.TerminateWorkflow(processId);
            return Ok();
        }

        [Route("Task/{taskId}")]
        [HttpPut]
        public IHttpActionResult ReassignWorkflow(int taskId, [FromBody]TaskReassignment body)
        {
            _workflowComponent.ReassignTask(taskId, body.AssigneeId);
            return Ok();
        }

        #endregion

        #region Delegation

        [Route("Delegations")]
        [HttpGet]
        public IHttpActionResult GetDelegations()
        {
            var delegation = _workflowComponent.GetMyDelegations();
            return Ok(delegation);
        }

        [Route("Delegation")]
        [HttpPost]
        public IHttpActionResult CreateDelegation(CreateDelegation delegation)
            {
            _workflowComponent.CreateDelegation(delegation);
            return Ok();
        }

        [Route("Delegation/{delegationId}")]
        [HttpDelete]
        public IHttpActionResult DeleteDelegation(int delegationId)
        {
            _workflowComponent.DeleteDelegation(delegationId);
            return Ok();
        }

        #endregion

        [Route("StartWorkflow")]
        [HttpPost]
        public IHttpActionResult StartWorkflow(StartWorkflowDTO startWorkflow)
        {
            var processId = _workflowComponent.StartWorkflow(startWorkflow.matrixName, startWorkflow.referenceKey, UserAccessControl.GetCurrentUserId());
            return Ok(processId);
        }

        [Route("Approve/{processId}/{taskid}")]
        [HttpPost]
        public IHttpActionResult Approve(int processId, int taskId)
        {
            _workflowComponent.Approve(processId, taskId);
            return Ok();
        }

        [Route("Reject/{processId}/{taskid}")]
        [HttpPost]
        public IHttpActionResult Reject(int processId, int taskId)
        {
            _workflowComponent.Reject(processId, taskId);
            return Ok();
        }

        [Route("Resubmit/{processId}/{taskid}")]
        [HttpPost]
        public IHttpActionResult Resubmit(int processId, int taskId)
        {
            _workflowComponent.Resubmit(processId, taskId);
            return Ok();
        }

        public class StartWorkflowDTO
        {
            public string matrixName { get; set; }

            public string referenceKey { get; set; }
        }
    }
}
