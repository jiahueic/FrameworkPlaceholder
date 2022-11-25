using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.Process;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.Group;
using AGTIV.Framework.MVC.UI.ViewModel.Student;
using AGTIV.Framework.MVC.UI.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentProcess _studentProcess;
        public StudentController(IStudentProcess studentProcess)
        {
            _studentProcess = studentProcess;
        }

        // GET: Student
        // the index page returns a list of students 
        [Authorize]
        public ActionResult Index()
        {

           StudentListViewModel pagedList = _studentProcess.GetAll();
            return View(pagedList);
  
        }
        [Authorize]
        public ActionResult Detail(int id)
        {
            StudentViewModel studentViewModel = _studentProcess.Get(id);
            return View(studentViewModel);
        }
        [Authorize]
        public ActionResult AddCourse(int id)
        {
            var vm = new CreateCourseViewModel();
            vm.StudentId= id;
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveCourse(CreateCourseViewModel vm)
        {
            _studentProcess.AddCourse(vm);
            return RedirectToAction("Index");
        }

    }
}