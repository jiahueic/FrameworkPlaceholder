using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Student
{
    public class StudentListViewModel
    {
        public List<StudentViewModel> StudentViewModels { get; set; }
        public StudentListViewModel()
        {
            StudentViewModels = new List<StudentViewModel>();
        }
    }
}
