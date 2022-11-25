using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGTIV.Framework.MVC.UI.ViewModel.Calendar
{
    public class HolidayVM
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }

        [Required]
        [DisplayName("Holiday Name")]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public int Days { get; set; }

        public HolidayVM()
        {
            Year = DateTime.Today.Year;
        }
    }
}