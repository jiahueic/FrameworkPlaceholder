using System;
using System.Runtime.Serialization;
using AGTIV.Framework.MVC.Entities.Shared;

namespace AGTIV.Framework.MVC.Entities.Maintenance
{
    [Serializable]
    [DataContract]
    public class CalendarProfileHoliday : Entity
    {
        [DataMember]
        public Guid HolidayId { get; set; }
        [DataMember]
        public Guid CalendarProfileId { get; set; }
        [DataMember]
        public string HolidayName { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int Days { get; set; }
    }
}
