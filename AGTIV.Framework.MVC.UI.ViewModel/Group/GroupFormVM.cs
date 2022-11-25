using AGTIV.Framework.MVC.UI.ViewModel.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGTIV.Framework.MVC.UI.ViewModel.Group
{
    public class GroupFormVM : GroupVM
    {
        [Required]
        public override string Name { get; set; }

        [DisplayName("Users")]
        public Guid[] UserIds { get; set; }

        [DisplayName("Roles")]
        public Guid[] RoleIds { get; set; }

        public IEnumerable<DropDownListItem> UserDdl { get; set; }

        public IEnumerable<DropDownListItem> RoleDdl { get; set; }
    }
}