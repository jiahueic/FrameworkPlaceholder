using AGTIV.Framework.MVC.UI.Process.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AGTIV.Framework.MVC.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE2MTkwQDMxMzcyZTM0MmUzME1DMFFIc28xcjhnakIwQTljTE9OVjdKYkpselNmazRjMzdjS0ZJcHE0V2c9;MjE2MTkxQDMxMzcyZTM0MmUzMEcrZnJ5anc5WW1FOE5ra3BQalRsaDNwblBEckp2WThzOUs2RmtGc3MzaTQ9");

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
