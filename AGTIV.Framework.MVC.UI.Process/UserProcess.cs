using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using AGTIV.Framework.MVC.UI.ViewModel.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class UserProcess : IUserProcess
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IAppSetting _appSetting;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IWebServiceExecutorFactory _serviceFactory;

        public UserProcess(IAPIHelper apiHelper, IAppSetting appSetting, IBearerTokenManager tokenManager, IWebServiceExecutorFactory serviceFactory)
        {
            _apiHelper = apiHelper;
            _appSetting = appSetting;
            _tokenManager = tokenManager;
            _serviceFactory = serviceFactory;
        }

        public IEnumerable<User> Get()
        {
            List<User> result = new List<User>();
            IWebServiceResponse<List<UserDto>> response = default(IWebServiceResponse<List<UserDto>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.User);
                response = _service.ExecuteRequest<List<UserDto>>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<User>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public User Get(Guid id)
        {
            User result = new User();
            IWebServiceResponse<User> response = default(IWebServiceResponse<User>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.UserById, id.ToString());
                response = _service.ExecuteRequest<User>(requestURL, HttpMethod.GET);
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

        public bool Create(CreateUserVM pVM)
        {
            bool result;
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.User);
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, pVM.User);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data;
            }
            else if(response.StatusCode == HttpStatusCode.Forbidden)
            {
                result = false;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void Update(User vm)
        {
            //List<User> result = new List<User>();
            //IWebServiceResponse<List<User>> response = default(IWebServiceResponse<List<User>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.User);
                _service.ExecuteRequest<List<User>>(requestURL, HttpMethod.PUT, vm);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    result = response.Data;
            //}
            //else
            //{
            //    throw new ProcessException(response.StatusCode, response.RawContent);
            //}

            //return result;
        }

        public void Delete(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.UserById, id.ToString());
                _service.ExecuteRequest<List<User>>(requestURL, HttpMethod.DELETE);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }
        }

        public UserInfoViewModel Login(string username, string password/*, bool rememberMe*/)
        {
            IWebServiceExecutor webServiceExecutor = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);
            IWebServiceResponse<UserInfoViewModel> response = default(IWebServiceResponse<UserInfoViewModel>);
            UserInfoViewModel userInfo = new UserInfoViewModel();

            var loginInfo = new
            {
                grant_type = "password",
                username,
                password,
                //rememberMe
                client_id = _appSetting.ClientId,
                client_secret = _appSetting.ClientSecret
            };

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Login);
                response = webServiceExecutor.ExecuteRequest<UserInfoViewModel>(requestURL, HttpMethod.POST, loginInfo);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                userInfo = response.Data;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized) // Token endpoint will return 401 when authentication rejected.
            {
                userInfo = null;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return userInfo;
        }

        public void ForgotPassword(string email)
        {
            IWebServiceExecutor webServiceExecutor = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email)
            };

            var queryString = ConversionHelper.ToQueryString(parameters, true);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.ForgotPassword) + queryString;
                response = webServiceExecutor.ExecuteRequest<bool>(requestURL, HttpMethod.POST);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }

        public void ResetPassword(ResetPasswordVM vm)
        {
            IWebServiceExecutor webServiceExecutor = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Basic.Value);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var dto = Mapper.Map<ResetPasswordDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.ResetPassword);
                response = webServiceExecutor.ExecuteRequest<bool>(requestURL, HttpMethod.POST, dto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public string ChangePassword(ChangePasswordVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<IdentityResultDto> response = default(IWebServiceResponse<IdentityResultDto>);
            string result = default(string);

            try
            {
                var dto = Mapper.Map<ChangePasswordDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.ChangePassword);
                response = _service.ExecuteRequest<IdentityResultDto>(requestURL, HttpMethod.POST, dto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data?.Errors.FirstOrDefault();
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public bool Signup(User user)
        {
            bool success = false;

            IWebServiceExecutor webServiceExecutor = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Signup);
                response = webServiceExecutor.ExecuteRequest<bool>(requestURL, HttpMethod.POST, user);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return success;
        }

        public void UploadProfilePicture(Image image)
        {
            bool success = false;

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.UploadProfilePicture);
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, image);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                success = true;
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
        }
    }
}
