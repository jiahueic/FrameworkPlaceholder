//using AGTIV.Framework.MVC.Business.UnitOfWork;
//using AGTIV.Framework.MVC.DTO.Maintenance;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AGTIV.Framework.MVC.UI.ViewModel.Student;
//using AGTIV.Framework.MVC.Framework.CredentialManager;

//namespace AGTIV.Framework.MVC.Business.Maintenance
//{
//    public class StudentComponent : IGroupComponent
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public StudentComponent(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }
//        StudentDto Get(int id)
//        {
//            var data = _unitOfWork.Repository.GetByID<Student>(id);
//            var student = MapToStudentDto(data);
//            return student;


//        }
//        List<StudentDto> GetAll()
//        {
//            var data = _unitOfWork.Repository.GetAll<Student>();
//            var groups = data.Select(g => MapToStudentDto(g)).ToList();
//            return groups;

//        }
//        void SaveCourse(StudentDto student)
//        {
//            var currentUserId = UserAccessControl.GetCurrentUserId();
//            var data = new Group
//            {
//                Name = group.Name,
//                CreatedBy = currentUserId,
//                ModifiedBy = currentUserId,
//            };

//            AttachUsersInGroup(group, data);
//            AttachRolesInGroup(group, data);
//            _unitOfWork.Repository.Insert(data);
//            _unitOfWork.Save();
//        }
//    }
//    private StudentDto MapToStudentDto(Student student)
//    {
//          List<CourseDto> courseDtoCollection = new List<CourseDto>();
//          for(int i = 0; i < student.Courses.Length; i ++) {
//              Course course = student.Courses[i];
//              CourseDto courseDto = MapCourseToDto(course);
//              courseDtoCollection.Add(courseDto);
//          }
//        return new StudentDto
//        {
//            Id = student.Id,
//            Name = student.Name,
//            University = student.University,
//            Age = student.Age,
//            Courses = courseDtoCollection;
//        };

//    }
//    private CourseDto MapToCourseDto(Course course)
//    {
//        return new CourseDto
//         {
//           Id = courseId,
//           CourseTitle = course.courseTitle,
//           Result = course.Result,
//         }
//}
