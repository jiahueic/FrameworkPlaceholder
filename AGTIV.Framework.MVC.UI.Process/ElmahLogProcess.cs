using AGTIV.Framework.MVC.Entities.ElmahLog;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel.ElmahLog;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public class ElmahLogProcess : IElmahLogProcess
    {
        private readonly IWebServiceExecutorFactory _serviceFactory;
        private readonly IBearerTokenManager _tokenManager;
        private readonly IAPIHelper _apiHelper;

        public ElmahLogProcess(
            IWebServiceExecutorFactory serviceFactory,
            IBearerTokenManager tokenManager,
            IAPIHelper apiHelper
            )
        {
            _serviceFactory = serviceFactory;
            _tokenManager = tokenManager;
            _apiHelper = apiHelper;
        }

        public PagedList<ElmahErrorVM> GetPaged(PagingRequest paging)
        {
            PagedList<ElmahErrorVM> result = new PagedList<ElmahErrorVM>();
            IWebServiceResponse<PagedList<Elmah_Error>> response = default(IWebServiceResponse<PagedList<Elmah_Error>>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetElmah);
                response = _service.ExecuteRequest<PagedList<Elmah_Error>>(requestURL, HttpMethod.POST, paging);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<PagedList<ElmahErrorVM>>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }

        public ElmahErrorVM GetError(Guid errorId)
        {
            ElmahErrorVM result = new ElmahErrorVM();
            IWebServiceResponse<Elmah_Error> response = default(IWebServiceResponse<Elmah_Error>);

            IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.BearerToken.Value, _tokenManager.AccessToken);
            //IWebServiceExecutor _service = _serviceFactory.CreateInstance(RestSharpWebServiceExecutorType.Form.Value);

            try
            {
                string requestURL = _apiHelper.GetAPIUrl(ConstantHelper.API.Path.GetElmahError, errorId.ToString());
                response = _service.ExecuteRequest<Elmah_Error>(requestURL, HttpMethod.GET);
            }
            catch (WebServiceException ex)
            {
                throw new ProcessException(ConstantHelper.Error.Common.WebServiceFailure, ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<ElmahErrorVM>(response.Data);
            }
            else
            {
                throw new ProcessException(response.StatusCode, response.RawContent);
            }

            return result;
        }
    }
}
