using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGTIV.Framework.MVC.Business.Maintenance
{
    public class CalendarComponent : ICalendarComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CalendarProfile> GetList()
        {
            var data = _unitOfWork.Repository.GetAll<CalendarProfile>();

            foreach (CalendarProfile calendarProfile in data)
            {
                if (calendarProfile.ParentProfileId.HasValue)
                {
                    var parentProfile = data.Where(x => x.Id == calendarProfile.ParentProfileId.Value).FirstOrDefault();

                    if (parentProfile != null)
                    {
                        calendarProfile.ParentProfileName = parentProfile.CalendarProfileName;
                    }
                }

                string workingDays = String.Empty;

                if (calendarProfile.Monday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Monday";
                    else
                        workingDays += ", Monday";
                }

                if (calendarProfile.Tuesday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Tuesday";
                    else
                        workingDays += ", Tuesday";
                }

                if (calendarProfile.Wednesday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Wednesday";
                    else
                        workingDays += ", Wednesday";
                }

                if (calendarProfile.Thursday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Thursday";
                    else
                        workingDays += ", Thursday";
                }

                if (calendarProfile.Friday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Friday";
                    else
                        workingDays += ", Friday";
                }

                if (calendarProfile.Saturday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Saturday";
                    else
                        workingDays += ", Saturday";
                }

                if (calendarProfile.Sunday)
                {
                    if (String.IsNullOrEmpty(workingDays))
                        workingDays = "Sunday";
                    else
                        workingDays += ", Sunday";
                }

                calendarProfile.WorkingDays = workingDays;

            }

            return data;
        }

        public CalendarProfileDto GetProfile(Guid id)
        {
            var data = _unitOfWork.Repository.GetByID<CalendarProfile>(id);
            var dto = MapCalendarProfileToDto(data);
            return dto;
        }

        public List<CalendarProfileDto> GetParentProfiles()
        {
            var data = _unitOfWork.Repository.Get<CalendarProfile>(x => x.IsParentProfile);
            var list = data.Select(x => MapCalendarProfileToDto(x)).ToList();
            return list;
        }

        public Guid CreateProfile(CalendarProfileDto calendarProfile)
        {
            var currentUserId = UserAccessControl.GetCurrentUserId();
            var data = new CalendarProfile
            {
                CreatedBy = currentUserId,
                ModifiedBy = currentUserId
            };
            MapDtoToCalendarProfile(calendarProfile, data);
            _unitOfWork.Repository.Insert(data);
            _unitOfWork.Save();
            return data.Id;
        }

        public void UpdateProfile(CalendarProfileDto calendarProfile)
        {
            var data = _unitOfWork.Repository.GetByID<CalendarProfile>(calendarProfile.Id);
            MapDtoToCalendarProfile(calendarProfile, data);
            data.ModifiedBy = UserAccessControl.GetCurrentUserId();
            data.ModifiedOn = DateTime.Now;
            _unitOfWork.Repository.Update(data);
            _unitOfWork.Save();
        }

        public void CreateHoliday(CalendarHolidayDto calendarHoliday)
        {
            var currentUserId = UserAccessControl.GetCurrentUserId();
            var data = new CalendarProfileHoliday();
            MapDtoToCalendarHoliday(calendarHoliday, data);
            data.CreatedBy = currentUserId;
            data.ModifiedBy = currentUserId;
            _unitOfWork.Repository.Insert(data);
            _unitOfWork.Save();
        }

        public void UpdateHoliday(CalendarHolidayDto calendarHoliday)
        {
            var data = _unitOfWork.Repository.GetByID<CalendarProfileHoliday>(calendarHoliday.Id);
            MapDtoToCalendarHoliday(calendarHoliday, data);
            data.ModifiedBy = UserAccessControl.GetCurrentUserId();
            data.ModifiedOn = DateTime.Now;
            _unitOfWork.Repository.Update(data);
            _unitOfWork.Save();
        }

        public void DeleteHoliday(Guid id)
        {
            _unitOfWork.Repository.Delete<CalendarProfileHoliday>(id);
            _unitOfWork.Save();
        }

        public CalendarHolidayDto GetHoliday(Guid id)
        {
            var data = _unitOfWork.Repository.GetByID<CalendarProfileHoliday>(id);
            var dto = MapCalendarHolidayToDto(data);
            return dto;
        }

        public PagedList<CalendarHolidayDto> GetHolidayPaged(Guid profileId, PagingRequest paging)
        {
            if (paging.Search != null)
            {
                foreach (var search in paging.Search)
                {
                    var sqlFields = new List<string>();

                    foreach (var item in search.Fields)
                        sqlFields.Add(GetSqlName(item));

                    search.Fields = sqlFields;
                }
            }

            if (paging.Sorted != null)
            {
                foreach (var item in paging.Sorted)
                    item.Name = GetSqlName(item.Name);
            }

            var query = _unitOfWork.Repository.GetQuery<CalendarProfileHoliday>(false, x => x.CalendarProfileId == profileId);
            var data = PagingHelper.GetPagedList(query, paging);
            var pagedList = new PagedList<CalendarHolidayDto>
            {
                Result = data.Result.Select(x => MapCalendarHolidayToDto(x)).ToList(),
                TotalCount = data.TotalCount
            };

            return pagedList;
        }

        public List<CalendarProfileDdlDto> GetCalendarProfileDdl()
        {
            List<CalendarProfileDdlDto> calendarProfileDdls = new List<CalendarProfileDdlDto>();

            var calendarProfiles = _unitOfWork.Repository.GetAll<CalendarProfile>();

            foreach (var calendarProfile in calendarProfiles)
            {
                CalendarProfileDdlDto calendarProfileDdl = new CalendarProfileDdlDto()
                {
                    Id = calendarProfile.Id,
                    Name = calendarProfile.CalendarProfileName
                };

                calendarProfileDdls.Add(calendarProfileDdl);
            }

            return calendarProfileDdls;
        }

        public int GetDueDaysBasedOnHolidays(int originalDueDays, DateTime startDate, Guid userId)
        {
            DateTime currentDate = startDate;
            DateTime currentDateUtc = startDate.ToUniversalTime();
            var calendarProfileId = _unitOfWork.Repository.GetByID<UserProfile>(userId).CalendarProfile_Id;

            if(calendarProfileId == null)
                return originalDueDays;

            var currentUserHolidayProfile = _unitOfWork.Repository.GetByID<CalendarProfile>(calendarProfileId);

            for(int i = 0; i < originalDueDays; i++)
            {
                bool isHoliday, isWorkingDay;

                if(currentUserHolidayProfile.ParentProfileId == null)
                {

                    isHoliday = _unitOfWork.Repository.Get<CalendarProfileHoliday>(x =>
                        x.StartDate >= currentDateUtc
                        && x.EndDate <= currentDateUtc
                        && x.CalendarProfileId == currentUserHolidayProfile.Id).Count() > 0
                        ? true
                        : false;
                }
                else
                {
                    isHoliday = _unitOfWork.Repository.Get<CalendarProfileHoliday>(x =>
                        x.StartDate >= currentDateUtc
                        && x.EndDate <= currentDateUtc
                        && (x.CalendarProfileId == currentUserHolidayProfile.Id 
                            || x.CalendarProfileId == currentUserHolidayProfile.ParentProfileId))
                        .Count() > 0
                        ? true
                        : false;
                }

                switch(currentDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        isWorkingDay = currentUserHolidayProfile.Sunday;
                        break;
                    case DayOfWeek.Monday:
                        isWorkingDay = currentUserHolidayProfile.Monday;
                        break;
                    case DayOfWeek.Tuesday:
                        isWorkingDay = currentUserHolidayProfile.Tuesday;
                        break;
                    case DayOfWeek.Wednesday:
                        isWorkingDay = currentUserHolidayProfile.Wednesday;
                        break;
                    case DayOfWeek.Thursday:
                        isWorkingDay = currentUserHolidayProfile.Thursday;
                        break;
                    case DayOfWeek.Friday:
                        isWorkingDay = currentUserHolidayProfile.Friday;
                        break;
                    case DayOfWeek.Saturday:
                        isWorkingDay = currentUserHolidayProfile.Saturday;
                        break;
                    default:
                        isWorkingDay = false;
                        break;
                }

                if(isHoliday || !isWorkingDay)
                    originalDueDays = originalDueDays + 1;

                currentDate = currentDate.AddDays(1);
                currentDateUtc = currentDateUtc.AddDays(1);
            }

            return originalDueDays;
        }

        private void MapDtoToCalendarProfile(CalendarProfileDto calendarProfile, CalendarProfile data)
        {
            data.ParentProfileId = calendarProfile.ParentProfileId;
            data.CalendarProfileName = calendarProfile.Name;
            data.Monday = calendarProfile.Monday;
            data.Tuesday = calendarProfile.Tuesday;
            data.Wednesday = calendarProfile.Wednesday;
            data.Thursday = calendarProfile.Thursday;
            data.Friday = calendarProfile.Friday;
            data.Saturday = calendarProfile.Saturday;
            data.Sunday = calendarProfile.Sunday;
            data.IsParentProfile = calendarProfile.IsParentProfile;
        }

        private CalendarProfileDto MapCalendarProfileToDto(CalendarProfile profile)
        {
            return new CalendarProfileDto
            {
                Id = profile.Id,
                Name = profile.CalendarProfileName,
                ParentProfileId = profile.ParentProfileId,
                Monday = profile.Monday,
                Tuesday = profile.Tuesday,
                Wednesday = profile.Wednesday,
                Thursday = profile.Thursday,
                Friday = profile.Friday,
                Saturday = profile.Saturday,
                Sunday = profile.Sunday,
                IsParentProfile = profile.IsParentProfile
            };
        }

        private void MapDtoToCalendarHoliday(CalendarHolidayDto calendarHoliday, CalendarProfileHoliday data)
        {
            data.CalendarProfileId = calendarHoliday.ProfileId;
            data.HolidayName = calendarHoliday.Name;
            data.StartDate = calendarHoliday.StartDate;
            data.EndDate = calendarHoliday.EndDate;
            data.Year = calendarHoliday.Year;
            data.Days = calendarHoliday.Days;
        }

        private CalendarHolidayDto MapCalendarHolidayToDto(CalendarProfileHoliday holiday)
        {
            return new CalendarHolidayDto
            {
                Id = holiday.Id,
                ProfileId = holiday.CalendarProfileId,
                Name = holiday.HolidayName,
                Year = holiday.Year,
                StartDate = holiday.StartDate,
                EndDate = holiday.EndDate,
                Days = holiday.Days
            };
        }

        private string GetSqlName(string property)
        {
            switch (property)
            {
                case "Name":
                    return "HolidayName";
                default:
                    return property;
            }
        }

        public void Delete(Guid id)
        {
            _unitOfWork.Repository.Delete<CalendarProfile>(id);
            var holidays = _unitOfWork.Repository.Get<CalendarProfileHoliday>(x => x.CalendarProfileId == id);
            foreach (var holiday in holidays)
            {
                _unitOfWork.Repository.Delete<CalendarProfileHoliday>(holiday.Id);
            }

            _unitOfWork.Save();
        }
    }
}