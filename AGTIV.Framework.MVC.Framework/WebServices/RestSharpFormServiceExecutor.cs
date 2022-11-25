using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpFormServiceExecutor : RestSharpServiceExecutor
    {
        public RestSharpFormServiceExecutor()
        {
        }

        protected override IRestRequest ConstructRequest(string path, HttpMethod method, params object[] objects)
        {
            string[] queryStrings = new string[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                queryStrings[i] = GetQueryString(objects[i]);
            }
            string data = String.Join("&", queryStrings);

            IRestRequest request = new RestRequest(path, MapHttpMethodToRestSharpMethod(method));
            request.Parameters.Clear();
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", data, ParameterType.RequestBody);

            return request;
        }

        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
