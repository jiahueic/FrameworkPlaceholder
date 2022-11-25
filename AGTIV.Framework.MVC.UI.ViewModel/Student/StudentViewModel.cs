using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Student
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<Course> CourseCollection { get; set; }
        public StudentViewModel() => CourseCollection = new List<Course>();
    }
}
