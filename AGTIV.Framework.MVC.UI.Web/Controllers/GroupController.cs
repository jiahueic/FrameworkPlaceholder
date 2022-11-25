using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using AGTIV.Framework.MVC.UI.Web.Attribute;
using AGTIV.Framework.MVC.UI.Web.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupProcess _groupProcess;
        private readonly IUserProcess _userProcess;
        private readonly IRoleProcess _roleProcess;

        public GroupController(
            IGroupProcess groupProcess,
            IUserProcess userProcess,
            IRoleProcess roleProcess
            )
        {
            _groupProcess = groupProcess;
            _userProcess = userProcess;
            _roleProcess = roleProcess;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GroupTable(PagingRequest paging)
        {
            if(paging.Sorted == null)
                paging.Sorted = new List<Framework.Paging.Sort>
                {
                    new Framework.Paging.Sort
                    {
                        Name = ConversionHelper.GetPropertyDisplayName<GroupVM>(g => g.Name, getPropertyName: true),
                        Direction = ConstantHelper.Paging.Ascending,
                    }
                };

            var pagedList = _groupProcess.GetAll(paging);
            return paging.RequiresCounts ? Json(new { result = pagedList.Result, count = pagedList.TotalCount }, JsonRequestBehavior.AllowGet) : Json(pagedList.Result, JsonRequestBehavior.AllowGet);
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult Create()
        {
            var vm = new GroupFormVM
            {
                UserDdl = _userProcess.Get().Select(x => new DropDownListItem { Text = x.FullName, Value = x.Id }),
                RoleDdl = _roleProcess.Get().Select(x => new DropDownListItem { Text = x.Name, Value = x.Id })
            };
            return View(vm);
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult Create(GroupFormVM vm)
        {
            _groupProcess.Create(vm);
            return RedirectToAction("Index");
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult Edit(Guid id)
        {
            var vm = _groupProcess.Get(id);
            vm.UserDdl = _userProcess.Get().Select(x => new DropDownListItem { Text = x.FullName, Value = x.Id });
            vm.RoleDdl = _roleProcess.Get().Select(x => new DropDownListItem { Text = x.Name, Value = x.Id });
            return View(vm);
        }

        [HttpPost]
        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult Edit(GroupFormVM vm)
        {
            var isSuccess = _groupProcess.Update(vm);

            if(isSuccess)
                ViewBag.SuccessMsg = ConstantHelper.Success.General.RecordUpdatedSuccessfully;

            return View(vm);
        }

        [RoleAuthorize(Roles = ConstantHelper.Role.Admin)]
        public ActionResult Delete(GridVM<GroupVM> value)
        {
            _groupProcess.Delete(new Guid(value.key.ToString()));
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}