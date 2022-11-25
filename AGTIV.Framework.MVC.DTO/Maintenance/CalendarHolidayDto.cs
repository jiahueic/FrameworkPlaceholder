using System;

namespace AGTIV.Framework.MVC.DTO.Maintenance
{
    public class CalendarHolidayDto
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Days { get; set; }
    }
}