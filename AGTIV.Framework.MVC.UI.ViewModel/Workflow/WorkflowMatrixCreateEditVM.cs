using AGTIV.Framework.MVC.UI.ViewModel.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class WorkflowMatrixCreateEditVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Matrix Name")]
        public string MatrixName { get; set; }

        //public WorkflowMatrixStageVM Stage1 { get; set; }
        //public WorkflowMatrixStageVM Stage2 { get; set; }
        //public WorkflowMatrixStageVM Stage3 { get; set; }
        //public WorkflowMatrixStageVM Stage4 { get; set; }
        //public WorkflowMatrixStageVM Stage5 { get; set; }
        //public WorkflowMatrixStageVM StageApproved { get; set; }
        //public WorkflowMatrixStageVM StageRejected { get; set; }

        [Display(Name = "Stage 1")]
        public Guid[] Stage1Approvers { get; set; }

        [Display(Name = "Stage 2")]
        public Guid[] Stage2Approvers { get; set; }

        [Display(Name = "Stage 3")]
        public Guid[] Stage3Approvers { get; set; }

        [Display(Name = "Stage 4")]
        public Guid[] Stage4Approvers { get; set; }

        [Display(Name = "Stage 5")]
        public Guid[] Stage5Approvers { get; set; }

        [Display(Name = "Approved CC")]
        public Guid[] StageApprovedRecipient { get; set; }

        [Display(Name = "Rejected CC")]
        public Guid[] StageRejectedApprovers { get; set; }

        public Guid Stage1Id { get; set; }
        public Guid Stage2Id { get; set; }
        public Guid Stage3Id { get; set; }
        public Guid Stage4Id { get; set; }
        public Guid Stage5Id { get; set; }
        public Guid StageApprovedId { get; set; }
        public Guid StageRejectedId { get; set; }

        public List<User.User> UserList { get; set; }

        public List<GroupVM> GroupList { get; set; }

        public List<Approvers> Approvers 
        { 
            get {
                List<Approvers> approvers = new List<Approvers>();
                approvers.AddRange(UserList.Select(x => new Approvers() { Id = x.Id, FullName = x.FullName }));
                approvers.AddRange(GroupList.Select(x => new Approvers() { Id = x.Id, FullName = x.Name }));

                return approvers;
            }
         }
    }

    public class Approvers
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
    }
}
