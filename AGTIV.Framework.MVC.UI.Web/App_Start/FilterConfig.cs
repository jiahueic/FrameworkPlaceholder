using AGTIV.Framework.MVC.UI.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute()); // Enforces global authentication.
            filters.Add(new GlobalHandleErrorAttribute());
        }
    }
}
