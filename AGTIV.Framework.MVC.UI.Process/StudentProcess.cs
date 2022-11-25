using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using AGTIV.Framework.MVC.UI.ViewModel.Student;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    // requests are formed here
    public class StudentProcess : IStudentProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;
        public StudentProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }
        public void AddCourse(CreateCourseViewModel createCourseViewModel)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);
            try
            {
                var studentDto = Mapper.Map<StudentDto>(createCourseViewModel);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.StudentWithId);
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, studentDto);
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
        public StudentListViewModel GetAll()
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<List<StudentDto>> response = default(IWebServiceResponse<List<StudentDto>>);
            StudentListViewModel result = default(StudentListViewModel);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.Students);
                response = _service.ExecuteRequest<List<StudentDto>>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<StudentListViewModel>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }
        public StudentViewModel Get(int id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<StudentDto> response = default(IWebServiceResponse<StudentDto>);
            StudentViewModel result = default(StudentViewModel);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.StudentWithId, id.ToString());
                response = _service.ExecuteRequest<StudentDto>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<StudentViewModel>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;

        }
    }
}
