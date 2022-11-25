using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AGTIV.Framework.MVC.Entities.User
{
    public class UserProfile : IEntity
    {
        [Key, ForeignKey("AppUser")]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string MobileNo { get; set; }

        [DataMember]
        public string NewNRIC { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PostCode { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Manager { get; set; }

        [NotMapped]
        [DataMember]
        public string[] Roles { get; set; }

        [NotMapped]
        [DataMember]
        public string RolesString { get; set; }

        [NotMapped]
        [DataMember]
        public Image Image { get; set; }

        public AppUser AppUser { get; set; }

        [DataMember]
        public Guid? CalendarProfile_Id { get; set; }

        public virtual CalendarProfile CalendarProfile { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public int Status { get; set; }
    }
}
