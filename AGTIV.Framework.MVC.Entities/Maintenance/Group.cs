using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AGTIV.Framework.MVC.Entities.Maintenance
{
    public class Group : Entity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        [DataMember]
        public virtual ICollection<AppRole> AppRoles { get; set; }
    }
}