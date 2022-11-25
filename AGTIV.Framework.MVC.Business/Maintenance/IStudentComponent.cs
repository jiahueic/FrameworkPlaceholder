using AGTIV.Framework.MVC.DTO.Maintenance;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Maintenance
{
    public interface IStudentComponent
    {
        StudentDto Get(int id);
        List<StudentDto> GetAll();
        void SaveCourse(StudentDto student);
        
    }
}
