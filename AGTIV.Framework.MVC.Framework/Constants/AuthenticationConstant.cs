using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class Auth
        {
            public static string OwinChallengeFlag = "X-Challenge";
            public static TimeSpan SessionTimeout = TimeSpan.FromDays(7); // 7 days
            public static TimeSpan SessionTimeoutRememberMe = TimeSpan.FromDays(30); // 30 days
        }

        public class Claims
        {
            public static string UserId = "Id";
            public static string Username = "Username";
            public static string FullName = "FullName";
            public static string UserGroups = "UserGroups";
            public static string Roles = "Roles";
            public static string Privileges = "Privileges";
            public static string Token = "Token";
            public static string TokenExpiresIn = "TokenExpiresIn";
        }
    }
}
