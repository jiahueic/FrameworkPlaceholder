using System;

namespace AGTIV.Framework.MVC.DTO.Maintenance
{
    public class SubRequestSubCategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid SubRequestCategoryId { get; set; }

        public SubRequestCategoryDto SubRequestCategory { get; set; }
    }
}