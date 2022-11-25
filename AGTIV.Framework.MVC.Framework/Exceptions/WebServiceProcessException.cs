using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Exceptions
{
    public class WebServiceProcessException : Exception
    {
        public Guid SalesId { get; set; }

        public ExceptionType ExceptionType { get; set; }

        public WebServiceProcessException() :
            base()
        { }

        public WebServiceProcessException(Guid salesId) :
            base()
        {
            this.SalesId = salesId;
        }

        public WebServiceProcessException(string message) :
            base(message)
        { }

        public WebServiceProcessException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
