using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Configurations
{
    public interface IAppSetting
    {
        string AG_EmailDefaultCredential { get; }
        string AG_EmailFrom { get; }
        string AG_EmailHost { get; }
        string AG_EmailPassword { get; }
        string AG_EmailPort { get; }
        string AG_EmailUseDefaultCredential { get; }
        string AG_EmailUsername { get; }
        string AG_EmailUseSSL { get; }
        string AG_EncKey { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string DBConnString { get; }
        string WebApiUrl { get; }
        string WebUrl { get; }
    }
}
