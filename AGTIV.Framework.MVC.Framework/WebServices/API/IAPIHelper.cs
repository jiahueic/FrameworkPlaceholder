using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.API
{
    public interface IAPIHelper
    {
        string WebAPIUrl { get; }

        string GetAPIUrl(string path, params string[] parameters);
    }
}
