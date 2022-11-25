using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class Error
        {
            public class Common
            {
                public const string WebServiceFailure = "Error connecting to web service or web service returned an error result.";
                public const string WebServiceError = "Error occured while processing request.";
                public const string PleaseTryAgain = "Please try again after a moment. Contact system administrator if this problem persists.";
            }

            public class User
            {
                public const string LoginFail = "Your account or password is incorrect";
                public const string DuplicateEmail = "Email has been used";
                public const string OldPasswordMismatch = "Old password is not matching";
            }

            public class Elmah
            {
                public const string CorrelationId = "Correlation Id";
            }
        }

        public class Success
        {
            public class User
            {
                public const string ChangePasswordSuccess = "Password has changed successfully";
            }

            public class General
            {
                public const string RecordUpdatedSuccessfully = "Record updated successfully";
            }
        }
    }
}
