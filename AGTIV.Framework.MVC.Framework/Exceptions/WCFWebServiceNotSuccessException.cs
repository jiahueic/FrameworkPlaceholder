using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Exceptions
{
    public class WCFWebServiceNotSuccessException : WCFWebServiceException
    {
        public WCFWebServiceNotSuccessException() :
            base()
        { }

        public WCFWebServiceNotSuccessException(string message, WCFWebServiceDetail detail) :
            base(message, detail)
        {
        }

        public WCFWebServiceNotSuccessException(Guid id, string message, WCFWebServiceDetail detail) :
            base(id, message, detail)
        {
        }

        public WCFWebServiceNotSuccessException(string message, Exception innerException) :
            base(message, innerException)
        { }

    }
}
