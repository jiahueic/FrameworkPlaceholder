using System;

namespace AGTIV.Framework.MVC.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string MobileNo { get; set; }

        public string EmailAddress { get; set; }

        public string NewNRIC { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Department { get; set; }

        public string Manager { get; set; }

        public string Password { get; set; }

        public Guid AppUser_Id { get; set; }

        public string[] Roles { get; set; }

        public Guid? CalendarProfile_Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Status { get; set; }

    }
}