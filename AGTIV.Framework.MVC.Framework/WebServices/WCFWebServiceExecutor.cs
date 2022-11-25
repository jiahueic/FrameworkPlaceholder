using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class WCFWebServiceExecutor : RestSharpServiceExecutor
    {
        protected override void ThrowExceptionIfError(IRestResponse response)
        {
            base.ThrowExceptionIfError(response);
        }
    }
}
