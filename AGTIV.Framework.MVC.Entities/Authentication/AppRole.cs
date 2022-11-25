using AGTIV.Framework.MVC.Entities.Maintenance;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Entities.Authentication
{
    public class AppRole : IdentityRole<Guid, AppUserRole>
    {
        [DataMember]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
