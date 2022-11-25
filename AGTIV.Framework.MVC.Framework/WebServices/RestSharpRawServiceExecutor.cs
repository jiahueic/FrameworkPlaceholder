using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpRawServiceExecutor : RestSharpServiceExecutor
    {
        private readonly string _contentType;

        public RestSharpRawServiceExecutor(string contentType)
        {
            this._contentType = contentType;
        }

        protected override IRestRequest ConstructRequest(string path, HttpMethod method, params object[] objects)
        {
            if (objects.Length == 0)
                throw new ArgumentException("Objects cannot be empty.", "objects");

            // Will only take the first object as string to pass in.
            string data = objects[0].ToString();

            IRestRequest request = new RestRequest(path, MapHttpMethodToRestSharpMethod(method));
            request.Parameters.Clear();
            request.AddHeader("Content-Type", _contentType);
            request.AddParameter(_contentType, data, ParameterType.RequestBody);

            return request;
        }
    }
}
