using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.Framework.Paging;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel.Calendar;
using AGTIV.Framework.MVC.UI.Web.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ICalendarProcess _calendarProcess;

        public CalendarController(ICalendarProcess calendarProcess)
        {
            _calendarProcess = calendarProcess;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalendarProfileTable(DataManagerRequest dm)
        {
            IEnumerable DataSource = _calendarProcess.GetList();
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0)
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }

            int count = DataSource.Cast<CalendarProfileVM>().Count();

            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);
            }

            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts ? Json(new { result = DataSource, count }, JsonRequestBehavior.AllowGet) : Json(DataSource, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var vm = new CalendarProfileFormVM
            {
                ParentProfileDdl = _calendarProcess.GetParentProfilesDdl()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarProfileFormVM vm)
        {
            var id = _calendarProcess.CreateProfile(vm);
            return RedirectToAction("Edit", new { id });
        }

        public ActionResult Edit(Guid id)
        {
            var vm = _calendarProcess.GetProfile(id);
            vm.ParentProfileDdl = _calendarProcess.GetParentProfilesDdl();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CalendarProfileFormVM vm)
        {
            _calendarProcess.UpdateProfile(vm);
            return RedirectToAction("Edit", new { id = vm.Id });
        }

        public ActionResult HolidayTable(PagingRequest paging, Guid id)
        {
            if(id == null)
                return null;

            if(paging.Sorted == null)
                paging.Sorted = new List<Framework.Paging.Sort>
                {
                    new Framework.Paging.Sort
                    {
                        Name = ConversionHelper.GetPropertyDisplayName<HolidayVM>(g => g.StartDate, getPropertyName: true),
                        Direction = ConstantHelper.Paging.Ascending,
                    }
                };

            var pagedList = _calendarProcess.GetHolidayPaged(id, paging);
            return paging.RequiresCounts ? Json(new { result = pagedList.Result, count = pagedList.TotalCount }, JsonRequestBehavior.AllowGet) : Json(pagedList.Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateHoliday(Guid id)
        {
            var vm = new HolidayVM
            {
                ProfileId = id
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHoliday(HolidayVM vm)
        {
            _calendarProcess.CreateHoliday(vm);
            return RedirectToAction("Edit", new { id = vm.ProfileId });
        }

        public ActionResult EditHoliday(Guid id)
        {
            var vm = _calendarProcess.GetHoliday(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHoliday(HolidayVM vm)
        {
            _calendarProcess.UpdateHoliday(vm);
            return RedirectToAction("EditHoliday", new { id = vm.Id });
        }

        public ActionResult DeleteHoliday(GridVM<HolidayVM> value)
        {
            _calendarProcess.DeleteHoliday(new Guid(value.key.ToString()));
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(GridVM<CalendarProfileVM> row)
        {
            _calendarProcess.Delete(new Guid(row.key.ToString()));
            return Json(row, JsonRequestBehavior.AllowGet);
        }
    }
}