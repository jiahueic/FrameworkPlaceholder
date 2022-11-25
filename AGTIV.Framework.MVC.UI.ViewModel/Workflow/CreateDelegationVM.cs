using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class CreateDelegationVM : DelegationVM
    {
        [DisplayName("Delegate From")]
        public Guid? DelegateFromId { get; set; }

        [DisplayName("Delegate To")]
        public Guid? DelegateToId { get; set; }

        public IEnumerable<User.User> UserDdl { get; set; }
    }
}