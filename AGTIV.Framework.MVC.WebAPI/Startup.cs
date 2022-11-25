using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AGTIV.Framework.MVC.WebAPI.Startup))]

namespace AGTIV.Framework.MVC.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
