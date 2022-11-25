using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Helper
{
    public static class LogHelper
    {
        public static string LogMessage(string message)
        {
            try
            {
                return Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception(message)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string LogMessage(Exception exMessage)
        {
            try
            {
                return Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(exMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
