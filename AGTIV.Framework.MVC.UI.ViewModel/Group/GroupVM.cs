using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AGTIV.Framework.MVC.UI.ViewModel.Group
{
    public class GroupVM
    {
        public Guid Id { get; set; }

        public virtual string Name { get; set; }

        [DisplayName("Users")]
        public string UsersNameList
        {
            get
            {
                return JoinStringList(Users.Select(u => u.FullName).ToList());
            }
        }

        [DisplayName("Roles")]
        public string RoleNameList
        {
            get
            {
                return JoinStringList(Roles.Select(u => u.Name).ToList());
            }
        }

        public List<User.User> Users { get; set; }

        public List<Role.Role> Roles { get; set; }

        private string JoinStringList(List<string> names)
        {
            return string.Join(", ", names);
        }
    }
}