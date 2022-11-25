using System;

namespace AGTIV.Framework.MVC.DTO.Maintenance
{
    public class CalendarProfileDto
    {
        public Guid Id { get; set; }

        public Guid? ParentProfileId { get; set; }

        public string Name { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        public bool IsParentProfile { get; set; }
    }
}