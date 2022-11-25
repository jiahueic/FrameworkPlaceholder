using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.DTO.Maintenance
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string University { get; set; }
        public int Age { get; set; }
        public List<CourseDto> Courses { get; set; }
    }
}
