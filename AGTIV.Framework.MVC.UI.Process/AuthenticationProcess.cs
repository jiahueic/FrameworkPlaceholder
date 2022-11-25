using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class AuthenticationProcess : IAuthenticationProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public AuthenticationProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper)
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        //public UserInfoViewModel Login(string username, string password, bool rememberMe)
        //{
        //    // OAuth token endpoint requires form submission instead of json, so we use a special implement of it.
        //    IWebServiceExecutor formService = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

        //    ProcessUserInfo result = new ProcessUserInfo();
        //    UserInfoViewModel realResult = new UserInfoViewModel();
        //    IWebServiceResponse<ProcessUserInfo> response = default(IWebServiceResponse<ProcessUserInfo>);

        //    var loginInfo = new
        //    {
        //        grant_type = "password",
        //        username = username,
        //        password = password,
        //        rememberMe = rememberMe,
        //    };

        //    try
        //    {
        //        string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Login);
        //        response = formService.ExecuteRequest<ProcessUserInfo>(requestURL, HttpMethod.POST, loginInfo);
        //    }
        //    catch (WebServiceException ex)
        //    {
        //        throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
        //    }

        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
                
        //    }
        //    else
        //    {
        //        throw new ProcessException(response.StatusCode, response.RawContent);
        //    }

        //    return realResult;
        //}
    }
}
