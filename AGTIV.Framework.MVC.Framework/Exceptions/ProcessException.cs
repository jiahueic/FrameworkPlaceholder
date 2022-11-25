using AGTIV.Framework.MVC.Framework.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Exceptions
{
    public class ProcessException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public ProcessException()
            : base()
        {

        }

        public ProcessException(HttpStatusCode StatusCode)
            : base()
        {
            this.StatusCode = StatusCode;
        }

        public ProcessException(string message)
            : base(message)
        {

        }

        public ProcessException(HttpStatusCode StatusCode, string message)
            : base(GetMessageContent(StatusCode, message))
        {
            this.StatusCode = StatusCode;
        }

        public ProcessException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public static string GetMessageContent(HttpStatusCode statusCode, string message)
        {
            var stripMessage = message;
            if(statusCode == HttpStatusCode.MethodNotAllowed)
            {
                var mnaContent = JsonConvert.DeserializeObject<MethodNotAllowedContent>(message);
                stripMessage = mnaContent.Message;
            }
            //else if(((int)statusCode).MatchAny((int)HttpStatusCode.BadRequest, (int)HttpStatusCode.NotFound, (int)HttpStatusCode.InternalServerError))
            //{
            //    stripMessage = message.Replace(" - ", "\n");
            //}
            return stripMessage;
        }
    }

    public class MethodNotAllowedContent
    {
        public string Message { get; set; }
    }
}
