using AGTIV.Framework.MVC.Business.ElmahLog;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [RoutePrefix("api/ElmahLog")]
    public class ElmahLogController : ApiController
    {
        private IElmahComponent _elmahComponent;
        public ElmahLogController(IElmahComponent elmahComponent)
        {
            _elmahComponent = elmahComponent;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult GetPagedElmah(PagingRequest paging)
        {
            var result = _elmahComponent.GetElmahErrors(paging);
            return Ok(result);
        }

        [Route("List")]
        [HttpPost]
        public IHttpActionResult GetPagedElmah()
        {
            var result = _elmahComponent.GetElmahErrors();
            return Ok(result);
        }

        [Route("{errorId}")]
        [HttpGet]
        public IHttpActionResult GetElmah(Guid errorId)
        {
            var result = _elmahComponent.GetElmahError(errorId);
            return Ok(result);
        }
    }
}
