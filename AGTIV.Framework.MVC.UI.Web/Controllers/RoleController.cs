using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.Role;
using AGTIV.Framework.MVC.UI.ViewModel.User;
using Syncfusion.EJ2.Base;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleProcess _roleProcess;
        private readonly IUserProcess _userProcess;

        public RoleController(
            IRoleProcess roleProcess,
            IUserProcess userProcess)
        {
            _roleProcess = roleProcess;
            _userProcess = userProcess;
        }

        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _GetRoleList()
        {
            var roleList = _roleProcess.Get();

            return Json(new { result = roleList, count = roleList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _GetUserList(DataManagerRequest dm)
        {
            var operation = new DataOperations();
            var userList = _userProcess.Get();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                userList = operation.PerformSearching(userList, dm.Search);  //Search
            }
            //if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            //{
            //    userList = operation.PerformSorting(userList, dm.Sorted);
            //}
            //if (dm.Where != null && dm.Where.Count > 0) //Filtering
            //{
            //    userList = operation.PerformFiltering(userList, dm.Where, dm.Where[0].Operator);
            //}
            int count = userList.Cast<User>().Count();
            if (dm.Skip != 0)
            {
                userList = operation.PerformSkip(userList, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                userList = operation.PerformTake(userList, dm.Take);
            }

            return Json(new { result = userList, count = count }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult _RoleAssignment(Guid id, string roles)
        {
            var roleDDL = _roleProcess.Get();

            var pVM = new RoleAssignmentPVM
            {
                UserId = id,
                RoleDLL = roleDDL,
                SelectedRole = roles.Split(',')
            };

            return PartialView(pVM);
        }

        [HttpPost]
        public ActionResult _RoleAssignment(RoleAssignmentPVM pVM)
        {
            _roleProcess.AssignRole(pVM);

            return RedirectToAction("Index");

        }
    }
}