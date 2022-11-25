using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.ViewModel.Calendar;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface ICalendarProcess
    {
        List<CalendarProfileVM> GetList();

        void UpdateProfile(CalendarProfileFormVM vm);

        Guid CreateProfile(CalendarProfileFormVM vm);

        CalendarProfileFormVM GetProfile(Guid id);

        List<DropDownListItem> GetParentProfilesDdl();

        void CreateHoliday(HolidayVM vm);

        void UpdateHoliday(HolidayVM vm);

        void DeleteHoliday(Guid id);

        HolidayVM GetHoliday(Guid id);

        PagedList<HolidayVM> GetHolidayPaged(Guid profileId, PagingRequest paging);

        void Delete(Guid id);

        IEnumerable<CalendarProfileDdl> GetCalendarProfileDdl();
    }
}