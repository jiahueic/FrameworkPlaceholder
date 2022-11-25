using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.Calendar;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class CalendarProcess : ICalendarProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public CalendarProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        public List<CalendarProfileVM> GetList()
        {
            List<CalendarProfileVM> result = new List<CalendarProfileVM>();
            IWebServiceResponse<List<CalendarProfileVM>> response = default(IWebServiceResponse<List<CalendarProfileVM>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileList);
                response = _service.ExecuteRequest<List<CalendarProfileVM>>(requestURL, HttpMethod.GET);
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

        public void UpdateProfile(CalendarProfileFormVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var calendarProfileDto = Mapper.Map<CalendarProfileDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileWithId, vm.Id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.PUT, calendarProfileDto);
            }
            catch(WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public Guid CreateProfile(CalendarProfileFormVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<Guid> response = default(IWebServiceResponse<Guid>);
            Guid result = default(Guid);

            try
            {
                var calendarProfileDto = Mapper.Map<CalendarProfileDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfile);
                response = _service.ExecuteRequest<Guid>(requestURL, HttpMethod.POST, calendarProfileDto);
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

        public CalendarProfileFormVM GetProfile(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<CalendarProfileDto> response = default(IWebServiceResponse<CalendarProfileDto>);
            CalendarProfileFormVM result = default(CalendarProfileFormVM);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileWithId, id.ToString());
                response = _service.ExecuteRequest<CalendarProfileDto>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<CalendarProfileFormVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public List<DropDownListItem> GetParentProfilesDdl()
        {
            List<DropDownListItem> result = new List<DropDownListItem>();
            IWebServiceResponse<List<CalendarProfileDto>> response = default(IWebServiceResponse<List<CalendarProfileDto>>);
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarParentProfiles);
                response = _service.ExecuteRequest<List<CalendarProfileDto>>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data.Select(x => new DropDownListItem
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList();
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }
            return result;
        }

        public void CreateHoliday(HolidayVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var calendarHolidayDto = Mapper.Map<CalendarHolidayDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileHoliday);
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.POST, calendarHolidayDto);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public void UpdateHoliday(HolidayVM vm)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                var calendarHolidayDto = Mapper.Map<CalendarHolidayDto>(vm);
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileHolidayWithId, vm.Id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.PUT, calendarHolidayDto);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public void DeleteHoliday(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileHolidayWithId, id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.DELETE);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public HolidayVM GetHoliday(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<CalendarHolidayDto> response = default(IWebServiceResponse<CalendarHolidayDto>);
            HolidayVM result = default(HolidayVM);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileHolidayWithId, id.ToString());
                response = _service.ExecuteRequest<CalendarHolidayDto>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<HolidayVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;

        }

        public PagedList<HolidayVM> GetHolidayPaged(Guid profileId, PagingRequest paging)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<PagedList<CalendarHolidayDto>> response = default(IWebServiceResponse<PagedList<CalendarHolidayDto>>);
            PagedList<HolidayVM> result = default(PagedList<HolidayVM>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileHolidays, profileId.ToString());
                response = _service.ExecuteRequest<PagedList<CalendarHolidayDto>>(requestURL, HttpMethod.POST, paging);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<PagedList<HolidayVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public void Delete(Guid id)
        {
            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            IWebServiceResponse<bool> response = default(IWebServiceResponse<bool>);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileDelete, id.ToString());
                response = _service.ExecuteRequest<bool>(requestURL, HttpMethod.DELETE);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ProcessException(response.StatusCode, response.RawContent);
        }

        public IEnumerable<CalendarProfileDdl> GetCalendarProfileDdl()
        {
            List<CalendarProfileDdl> result = new List<CalendarProfileDdl>();
            IWebServiceResponse<List<CalendarProfileDdl>> response = default(IWebServiceResponse<List<CalendarProfileDdl>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.CalendarProfileDdl);
                response = _service.ExecuteRequest<List<CalendarProfileDdl>>(requestURL, HttpMethod.GET);
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