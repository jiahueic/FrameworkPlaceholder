using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using AGTIV.Framework.MVC.Entities.Shared;

namespace AGTIV.Framework.MVC.Entities.Maintenance
{
    [Serializable]
    [DataContract]
    public class CalendarProfile : Entity
    {
        [DataMember]
        public Guid CalendarProfileId { get; set; }
        [DataMember]
        public Nullable<Guid> ParentProfileId { get; set; }
        [DataMember]
        public string CalendarProfileName { get; set; }
        [DataMember]
        public bool Monday { get; set; }
        [DataMember]
        public bool Tuesday { get; set; }
        [DataMember]
        public bool Wednesday { get; set; }
        [DataMember]
        public bool Thursday { get; set; }
        [DataMember]
        public bool Friday { get; set; }
        [DataMember]
        public bool Saturday { get; set; }
        [DataMember]
        public bool Sunday { get; set; }
        [DataMember]
        public bool IsParentProfile { get; set; }

        [NotMapped]
        [DataMember]
        public string ParentProfileName { get; set; }
        [NotMapped]
        [DataMember]
        public string WorkingDays { get; set; }
    }
}
