using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Exceptions
{
    public class WCFWebServiceException : Exception
    {
        public Guid SalesId { get; set; }

        public ExceptionType ExceptionType { get; set; }

        public Guid LotPurchaseId { get; set; }

        public WCFWebServiceDetail Detail { get; set; }

        public WCFWebServiceException() :
            base()
        { }

        public WCFWebServiceException(string message, WCFWebServiceDetail detail) :
            base(message)
        {
            this.Detail = detail;
        }

        public WCFWebServiceException(Guid id, string message, WCFWebServiceDetail detail) :
            base(message)
        {
            this.LotPurchaseId = this.SalesId = id;
            this.Detail = detail;
        }

        public WCFWebServiceException(string message, Exception innerException) :
            base(message, innerException)
        { }

    }

    public class WCFWebServiceDetail
    {
        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public string RequestContent { get; set; }

        public DateTime RequestDateTime { get; set; }

        public DateTime ResponseDateTime { get; set; }

        public HttpStatusCode ResponseCode { get; set; }

        public string ResponseContent { get; set; }

        public Exception Exception { get; set; }
    }

    public enum ExceptionType
    {
        MHW,
        Argument
    }
}
