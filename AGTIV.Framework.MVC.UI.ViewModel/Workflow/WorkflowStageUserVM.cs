using System;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowStageUserVM
    {
        public Guid Id { get; set; }

        public Guid StageId { get; set; }

        public Guid UserId { get; set; }

        public User.User User { get; set; }
    }
}
