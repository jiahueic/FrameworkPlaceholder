using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class RoleProcess : IRoleProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public RoleProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        public IEnumerable<Role> Get()
        {
            List<Role> result = new List<Role>();
            IWebServiceResponse<List<Role>> response = default(IWebServiceResponse<List<Role>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Role);
                response = _service.ExecuteRequest<List<Role>>(requestURL, HttpMethod.GET);
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

        public Guid AssignRole(RoleAssignmentPVM pVM)
        {
            Guid result = new Guid();
            IWebServiceResponse<Guid> response = default(IWebServiceResponse<Guid>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Role);
                response = _service.ExecuteRequest<Guid>(requestURL, HttpMethod.POST, pVM);
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
    }
}
