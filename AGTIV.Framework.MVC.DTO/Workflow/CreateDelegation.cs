using System;

namespace AGTIV.Framework.MVC.DTO.Workflow
{
    public class CreateDelegation : Delegation
    {
        public Guid DelegateFromId { get; set; }

        public Guid DelegateToId { get; set; }
    }
}