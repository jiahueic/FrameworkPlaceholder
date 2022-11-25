using AGTIV.Framework.MVC.Business.Maintenance;
using AGTIV.Framework.MVC.DTO.Maintenance;
using AGTIV.Framework.MVC.Framework.Paging;
using System;
using System.Web.Http;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Calendar")]
    public class CalendarController : ApiController
    {
        private readonly ICalendarComponent _calendarComponent;

        public CalendarController(ICalendarComponent calendarComponent)
        {
            _calendarComponent = calendarComponent;
        }

        [HttpGet]
        [Route("List")]
        public IHttpActionResult GetList()
        {
            var result = _calendarComponent.GetList();
            return Ok(result);
        }

        [HttpGet]
        [Route("ParentProfiles")]
        public IHttpActionResult GetParentProfiles()
        {
            var result = _calendarComponent.GetParentProfiles();
            return Ok(result);
        }

        public IHttpActionResult Post(CalendarProfileDto calendarProfile)
        {
            var id = _calendarComponent.CreateProfile(calendarProfile);
            return Ok(id);
        }

        [Route("{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var dto = _calendarComponent.GetProfile(id);
            return Ok(dto);
        }

        [Route("{id}")]
        public IHttpActionResult Put(Guid id, CalendarProfileDto calendarProfile)
        {
            if (id != calendarProfile.Id)
                throw new Exception("Calendar Profile ID is invalid.");

            _calendarComponent.UpdateProfile(calendarProfile);
            return Ok(true);
        }

        [Route("{id}/Holidays")]
        [HttpPost]
        public IHttpActionResult GetHolidayPaged(Guid id, PagingRequest paging)
        {
            var pagedList = _calendarComponent.GetHolidayPaged(id, paging);
            return Ok(pagedList);
        }

        [Route("Holiday")]
        [HttpPost]
        public IHttpActionResult CreateHoliday(CalendarHolidayDto calendarHoliday)
        {
            _calendarComponent.CreateHoliday(calendarHoliday);
            return Ok(true);
        }

        [Route("Holiday/{id}")]
        [HttpGet]
        public IHttpActionResult GetHoliday(Guid id)
        {
            var dto = _calendarComponent.GetHoliday(id);
            return Ok(dto);
        }

        [Route("Holiday/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateHoliday(Guid id, CalendarHolidayDto calendarHoliday)
        {
            if (id != calendarHoliday.Id)
                throw new Exception("Calendar Holiday ID is invalid.");

            _calendarComponent.UpdateHoliday(calendarHoliday);
            return Ok(true);
        }

        [Route("Holiday/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteHoliday(Guid id)
        {
            _calendarComponent.DeleteHoliday(id);
            return Ok(true);
        }

        [Route("Delete/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            _calendarComponent.Delete(id);
            return Ok(true);
        }

        [Route("Dropdown")]
        public IHttpActionResult GetCalendarProfileDdl()
        {
            var result = _calendarComponent.GetCalendarProfileDdl();
            return Ok(result);
        }
    }
}