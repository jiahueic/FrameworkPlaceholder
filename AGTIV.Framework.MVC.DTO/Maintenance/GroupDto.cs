using AGTIV.Framework.MVC.DTO.Role;
using AGTIV.Framework.MVC.DTO.User;
using System;
using System.Collections.Generic;

namespace AGTIV.Framework.MVC.DTO.Maintenance
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<UserDto> Users { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}