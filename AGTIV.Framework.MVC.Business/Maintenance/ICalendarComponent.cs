using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Entities.Maintenance;
using System.Collections.Generic;
using AGTIV.Framework.MVC.Framework.Paging;
using System;

namespace AGTIV.Framework.MVC.Business.Maintenance
{
    public interface ICalendarComponent
    {
        List<CalendarProfile> GetList();

        CalendarProfileDto GetProfile(Guid id);

        List<CalendarProfileDto> GetParentProfiles();

        Guid CreateProfile(CalendarProfileDto calendarProfile);

        void UpdateProfile(CalendarProfileDto calendarProfile);

        void CreateHoliday(CalendarHolidayDto calendarHoliday);

        void UpdateHoliday(CalendarHolidayDto calendarHoliday);

        void DeleteHoliday(Guid id);

        CalendarHolidayDto GetHoliday(Guid id);

        PagedList<CalendarHolidayDto> GetHolidayPaged(Guid profileId, PagingRequest paging);

        void Delete(Guid id);

        List<CalendarProfileDdlDto> GetCalendarProfileDdl();

        int GetDueDaysBasedOnHolidays(int originalDueDays, DateTime startDate, Guid userId);
    }
}