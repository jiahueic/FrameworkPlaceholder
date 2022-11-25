using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGTIV.Framework.MVC.UI.ViewModel.Calendar
{
    public class CalendarProfileVM
    {
        public Guid Id { get; set; }

        public virtual string Name { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        [DisplayName("Calendar Profile Name")]
        public string CalendarProfileName { get; set; }
        [DisplayName("Parent Profile Name")]
        public string ParentProfileName { get; set; }
        [DisplayName("Working Days")]
        public string WorkingDays { get; set; }
    }
}