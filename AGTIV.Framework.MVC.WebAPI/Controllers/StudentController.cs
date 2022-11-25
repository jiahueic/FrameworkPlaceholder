//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Http;
//using AGTIV.Framework.MVC.DTO.Maintenance;

//namespace AGTIV.Framework.MVC.WebAPI.Controllers
//{
//    [System.Web.Http.Authorize]
//    [System.Web.Http.RoutePrefix("api/Student")]
//    public class StudentController : ApiController
//    {
//private readonly _studentComponent;
//public StudentController(IStudentComponent groupComponent)
//{
//     _studentComponent = studentComponent;
//}
//        // GET: Student
//        [System.Web.Http.Route("{id}")]
//        public IHttpActionResult Get()
//        {
//            var student = _studentComponent.Get(id);
//            return Ok(student);
//        }
//        [System.Web.Http.Route("~/api/Students")]
//        public IHttpActionResult GetAll()
//        {
            //var students = _studendtComponent.GetAll();
            //return students;    
//        }

//        public IHttpActionResult SaveCourse(StudentDto studentDto) {
//          _studentComponent.Create(studentDto);
//          return OK(true);
//        }

//    }
//}