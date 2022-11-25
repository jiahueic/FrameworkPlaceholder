using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Student
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [CourseTitleValidation]
        public String CourseTitle { get; set; }
        [Required]
        public int Result { get; set; }
    }
    public class CourseTitleValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide course title");
            }
            else
            {
                string strVal = value.ToString();
                string symbols = "~!@#$%^&*()_+";
                for (int i = 0; i < symbols.Length; i++)
                {
                    if (strVal.Contains(symbols[i]))
                    {
                        return new ValidationResult("Course title should not contain symbols");
                    }
                }
            }

            return ValidationResult.Success;

        }
    }
}
