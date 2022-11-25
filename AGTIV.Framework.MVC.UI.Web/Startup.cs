using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AGTIV.Framework.MVC.UI.Web.Startup))]
namespace AGTIV.Framework.MVC.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
