using System;
using System.ComponentModel;

namespace AGTIV.Framework.MVC.UI.ViewModel.Workflow
{
    public class DelegationVM
    {
        public int DelegationID { get; set; }

        [DisplayName("Delegation From")]
        public string DelegationFrom { get; set; }

        [DisplayName("Delegation To")]
        public string DelegationTo { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
    }
}