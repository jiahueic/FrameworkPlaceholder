using AGTIV.Framework.MVC.DTO.Workflow;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.Workflow;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class WorkflowProcess : IWorkflowProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public WorkflowProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        public List<WorkflowConfigurationVM> GetWorkflowConfigurations()
        {
            List<WorkflowConfigurationVM> result = new List<WorkflowConfigurationVM>();
            IWebServiceResponse<List<WorkflowConfiguration>> response = default(IWebServiceResponse<List<WorkflowConfiguration>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetWorkflowConfigurations);
                response = _service.ExecuteRequest<List<WorkflowConfiguration>>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<WorkflowConfigurationVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public WorkflowConfigurationVM GetWorkflowConfiguration(int stepId, int actionId)
        {
            WorkflowConfigurationVM result = new WorkflowConfigurationVM();
            IWebServiceResponse<WorkflowConfiguration> response = default(IWebServiceResponse<WorkflowConfiguration>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetWorkflowConfiguration, stepId.ToString(), actionId.ToString());

                response = _service.ExecuteRequest<WorkflowConfiguration>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<WorkflowConfigurationVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void UpdateWorkflowConfiguration(WorkflowConfigurationVM model)
        {
            IWebServiceResponse<WorkflowConfiguration> response = default(IWebServiceResponse<WorkflowConfiguration>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                var configuration = Mapper.Map<WorkflowConfiguration>(model);

                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.UpdateWorkflowConfiguration);
                response = _service.ExecuteRequest<WorkflowConfiguration>(requestURL, HttpMethod.POST, configuration);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public List<WorkflowMatrixVM> GetWorkflowMatrices()
        {
            List<WorkflowMatrixVM> result = new List<WorkflowMatrixVM>();
            IWebServiceResponse<List<WorkflowMatrix>> response = default(IWebServiceResponse<List<WorkflowMatrix>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetWorkflowMatrices);
                response = _service.ExecuteRequest<List<WorkflowMatrix>>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<WorkflowMatrixVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public WorkflowMatrixVM GetWorkflowMatrix(Guid id)
        {
            WorkflowMatrixVM result = new WorkflowMatrixVM();
            IWebServiceResponse<WorkflowMatrix> response = default(IWebServiceResponse<WorkflowMatrix>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetWorkflowMatrix, id.ToString());
                response = _service.ExecuteRequest<WorkflowMatrix>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<WorkflowMatrixVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public Guid CreateEditWorkflowMatrix(WorkflowMatrixVM model)
        {
            Guid result = new Guid();
            IWebServiceResponse<Guid> response = default(IWebServiceResponse<Guid>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                var matrix = Mapper.Map<WorkflowMatrix>(model);

                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CreateEditWorkflowMatrix);
                response = _service.ExecuteRequest<Guid>(requestURL, HttpMethod.POST, matrix);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }
        
        public void DeleteWorkflowMatrix(Guid id)
        {
            WorkflowMatrixVM result = new WorkflowMatrixVM();
            IWebServiceResponse<WorkflowMatrix> response = default(IWebServiceResponse<WorkflowMatrix>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.DeleteWorkflowMatrix, id.ToString());
                response = _service.ExecuteRequest<WorkflowMatrix>(requestURL, HttpMethod.DELETE);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        #region Workflow Detail
        public int StartWorkflow(string matrixName, string referenceKey)
        {
            int result = default(int);
            IWebServiceResponse<int> response = default(IWebServiceResponse<int>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.StartWorkflow, matrixName, referenceKey);
                response = _service.ExecuteRequest<int>(requestURL, HttpMethod.POST);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void ApproveRequest(int processId, int taskId)
        {
            IWebServiceResponse response = default(IWebServiceResponse);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.ApproveRequest, processId.ToString(), taskId.ToString());
                response = _service.ExecuteRequest<int>(requestURL, HttpMethod.POST);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public void RejectRequest(int processId, int taskId)
        {
            IWebServiceResponse response = default(IWebServiceResponse);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.RejectRequest, processId.ToString(), taskId.ToString());
                response = _service.ExecuteRequest<int>(requestURL, HttpMethod.POST);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public void ResubmitRequest(int processId, int taskId)
        {
            IWebServiceResponse response = default(IWebServiceResponse);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.ResubmitRequest, processId.ToString(), taskId.ToString());
                response = _service.ExecuteRequest<int>(requestURL, HttpMethod.POST);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public WorkflowProcessVM GetProcess(int processId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<WorkflowProcessDto> response = default(IWebServiceResponse<WorkflowProcessDto>);
            WorkflowProcessVM result = default(WorkflowProcessVM);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowProcess, processId.ToString());
                response = service.ExecuteRequest<WorkflowProcessDto>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<WorkflowProcessVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public List<WorkflowHistoryVM> GetWorkflowHistories(int processId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<List<WorkflowHistory>> response = default(IWebServiceResponse<List<WorkflowHistory>>);
            List<WorkflowHistoryVM> result = default(List<WorkflowHistoryVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowProcessHistories, processId.ToString());
                response = service.ExecuteRequest<List<WorkflowHistory>>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<WorkflowHistoryVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public List<WorkflowLogVM> GetWorkflowLogs(int processId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<List<WorkflowLogDto>> response = default(IWebServiceResponse<List<WorkflowLogDto>>);
            List<WorkflowLogVM> result = default(List<WorkflowLogVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowProcessLogs, processId.ToString());
                response = service.ExecuteRequest<List<WorkflowLogDto>>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<WorkflowLogVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void TerminateWorkflow(int processId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowProcess, processId.ToString());
                response = service.ExecuteRequest<bool>(requestURL, HttpMethod.DELETE);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public void ReassignWorkflow(int taskId, Guid assigneeId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            var body = new TaskReassignment { AssigneeId = assigneeId };

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowTask, taskId.ToString());
                response = service.ExecuteRequest<bool>(requestURL, HttpMethod.PUT, body);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);

        }

        #endregion

        #region Delegation

        public List<DelegationVM> GetMyDelegations()
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<List<Delegation>> response = default(IWebServiceResponse<List<Delegation>>);
            List<DelegationVM> result = default(List<DelegationVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowDelegations);
                response = service.ExecuteRequest<List<Delegation>>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<DelegationVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void CreateDelegation(CreateDelegationVM vm)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var createDelegationDto = Mapper.Map<CreateDelegation>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowDelegation);
                response = service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, createDelegationDto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public void DeleteDelegation(int delegationId)
        {
            IWebServiceExecutor service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.WorkflowDelegationWithId, delegationId.ToString());
                response = service.ExecuteRequest<bool>(requestURL, HttpMethod.DELETE);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }
        #endregion
    }
}
