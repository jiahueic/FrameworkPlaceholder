using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpServiceExecutorWithAuthBson : RestSharpServiceExecutorWithAuth
    {
        public RestSharpServiceExecutorWithAuthBson(IAuthenticator _authenticator) : base(_authenticator)
        {
        }

        protected override IRestClient ConstructClient(string baseUrl)
        {
            IRestClient client = base.ConstructClient(baseUrl);

            // Add a deserializer for Bson data type.
            client.AddHandler("application/bson", new BsonDeserializer());

            return client;
        }

        protected override IRestRequest ConstructRequest(string path, HttpMethod method, params object[] objects)
        {
            /*
             * Only the first object will be serialized as BSON at the moment. Improve this if necessary.
             */
            IRestRequest request = new RestRequest(path, MapHttpMethodToRestSharpMethod(method));

            request.Parameters.Clear();

            if (objects.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var bsonWriter = new BsonDataWriter(memoryStream))
                    {
                        var serializer = new JsonSerializer();

                        serializer.Serialize(bsonWriter, objects[0]);
                        var bytes = memoryStream.ToArray();

                        request.AddParameter("application/bson", bytes, ParameterType.RequestBody);

                    }
                }
            }

            request.AddHeader("Accept", "application/bson");
            //request.AddHeader("Accept", "application/bson, application/json, text/json, text/x-json");
            request.AddHeader("Content-Type", "application/bson");

            return request;
        }

        /// <summary>
        /// Using JSON.NET to deserialize BSON.
        /// Referenced from http://stackoverflow.com/questions/18357134/how-can-i-make-restsharp-use-bson
        /// </summary>
        public class BsonDeserializer : IDeserializer
        {
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string DateFormat { get; set; }

            public T Deserialize<T>(IRestResponse response)
            {
                using (var memoryStream = new MemoryStream(response.RawBytes))
                {
                    using (var bsonReader = new BsonDataReader(memoryStream))
                    {
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(bsonReader);
                    }
                }
            }
        }
    }
}
