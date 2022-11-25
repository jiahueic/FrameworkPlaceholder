using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGTIV.Framework.MVC.UI.ViewModel.Student;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
   public interface IStudentProcess
    {
         void AddCourse(CreateCourseViewModel createCourseViewModel);
        StudentListViewModel GetAll();
        StudentViewModel Get(int id);
    }
}
