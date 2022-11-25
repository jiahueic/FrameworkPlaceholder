using System;

namespace AGTIV.Framework.MVC.DTO.Workflow
{
    public class Delegation
    {
        public int DelegationID { get; set; }

        public string DelegationFrom { get; set; }

        public string DelegationTo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}