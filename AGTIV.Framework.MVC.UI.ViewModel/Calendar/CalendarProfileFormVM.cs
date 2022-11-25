using AGTIV.Framework.MVC.UI.ViewModel.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGTIV.Framework.MVC.UI.ViewModel.Calendar
{
    public class CalendarProfileFormVM : CalendarProfileVM
    {
        [Required]
        public override string Name { get; set; }

        [DisplayName("Parent Profile")]
        public Guid? ParentProfileId { get; set; }

        [DisplayName("Is Parent Profile")]
        public bool IsParentProfile { get; set; }

        public IEnumerable<DropDownListItem> ParentProfileDdl { get; set; }
    }
}