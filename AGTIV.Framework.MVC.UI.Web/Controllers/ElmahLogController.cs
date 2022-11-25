using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.ElmahLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    public class ElmahLogController : Controller
    {
        private readonly IElmahLogProcess _elmahProcess;

        public ElmahLogController(IElmahLogProcess elmahProcess)
        {
            _elmahProcess = elmahProcess;
        }

        // GET: ElmahLog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ElmahTable(PagingRequest paging)
        {
            var pagedList = _elmahProcess.GetPaged(paging);
            return paging.RequiresCounts ? Json(new { result = pagedList.Result, count = pagedList.TotalCount }, JsonRequestBehavior.AllowGet) : Json(pagedList.Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ElmahDetail(Guid errorId)
        {
            var model = _elmahProcess.GetError(errorId);
            model.Error = ConversionHelper.XMLDeserialize<XMLError>(model.AllXml);
            return View(model);
        }

        public ActionResult XML(Guid errorId)
        {
            var model = _elmahProcess.GetError(errorId);
            return Content(model.AllXml, "text/xml");
        }

        public ActionResult Json(Guid errorId)
        {
            var model = _elmahProcess.GetError(errorId);
            model.Error = ConversionHelper.XMLDeserialize<XMLError>(model.AllXml);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(model.Error), "application/json");
        }
    }
}