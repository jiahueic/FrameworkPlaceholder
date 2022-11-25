using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class GroupProcess : IGroupProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public GroupProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        public void Create(GroupFormVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var groupDto = Mapper.Map<GroupDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Group);
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, groupDto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public bool Update(GroupFormVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var groupDto = Mapper.Map<GroupDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GroupwithId, vm.Id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.PUT, groupDto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public void Delete(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GroupwithId, id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.DELETE);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public GroupFormVM Get(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<GroupDto> response = default(IWebServiceResponse<GroupDto>);
            GroupFormVM result = default(GroupFormVM);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GroupwithId, id.ToString());
                response = _service.ExecuteRequest<GroupDto>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<GroupFormVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public List<GroupVM> GetAll()
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<List<GroupDto>> response = default(IWebServiceResponse<List<GroupDto>>);
            List<GroupVM> result = default(List<GroupVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Groups);
                response = _service.ExecuteRequest<List<GroupDto>>(requestURL, HttpMethod.GET);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<GroupVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public PagedList<GroupVM> GetAll(PagingRequest paging)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<PagedList<GroupDto>> response = default(IWebServiceResponse<PagedList<GroupDto>>);
            PagedList<GroupVM> result = default(PagedList<GroupVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Groups);
                response = _service.ExecuteRequest<PagedList<GroupDto>>(requestURL, HttpMethod.POST, paging);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<PagedList<GroupVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }
    }
}
