using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AGTIV.Framework.MVC.UI.ViewModel.User;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class ReassignTaskVM
    {
        public int TaskId { get; set; }

        [Required]
        [DisplayName("Assignee")]
        public Guid? AssigneeId { get; set; }

        public IEnumerable<User.User> UserDdl { get; set; }
    }
}