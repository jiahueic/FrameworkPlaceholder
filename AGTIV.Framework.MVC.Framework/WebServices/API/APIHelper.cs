using AGTIV.Framework.MVC.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.API
{
    public class APIHelper : IAPIHelper
    {
        private readonly IAppSetting _appSetting;

        public APIHelper(
            IAppSetting setting)
        {
            _appSetting = setting;
        }

        public string WebAPIUrl
        {
            get
            {
                return _appSetting.WebApiUrl;
            }
        }

        public string GetAPIUrl(string path, params string[] parameters)
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
