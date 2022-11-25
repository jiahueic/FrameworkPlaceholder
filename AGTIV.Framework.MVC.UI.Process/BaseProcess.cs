using AGTIV.Framework.MVC.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process
{
    public abstract class BaseProcess
    {
        protected readonly IAppSetting _setting;

        protected virtual string WebAPIUrl
        {
            get
            {
                return _setting.WebApiUrl;
            }
        }

        protected BaseProcess(IAppSetting setting)
        {
            _setting = setting;
        }

        protected string GetApiUrl(string path, params string[] parameters)
        {
            StringBuilder str = new StringBuilder();
            str.Append(WebAPIUrl);
            str.Append(path);

            for (int i = 0; i < parameters.Length; i++)
            {
                str.Replace("{" + i + "}", parameters[i]);
            }

            return str.ToString();
        }
    }
}
