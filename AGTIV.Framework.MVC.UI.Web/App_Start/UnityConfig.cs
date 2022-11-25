using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.WebServices;
using AGTIV.Framework.MVC.Framework.WebServices.API;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using AGTIV.Framework.MVC.UI.Process;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.Web.Manager;
using AGTIV.Framework.MVC.UI.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AGTIV.Framework.MVC.UI.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAppSetting, AppSetting>();
            container.RegisterType<IAPIHelper, APIHelper>();

            // Web service
            container.RegisterType<IRestSharpAuthenticatorFactory, RestSharpAuthenticatorFactory>();
            container.RegisterType<IWebServiceExecutor, RestSharpServiceExecutor>();
            container.RegisterType<IWebServiceExecutorFactory, RestSharpWebServiceExecutorFactory>();
            container.RegisterType<IWebServiceResponse, WebServiceResponse>();

            container.RegisterType<IAuthorizationManager, AuthorizationManager>();
            container.RegisterType<IBearerTokenManager, AuthorizationManager>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();

            // Process
            container.RegisterType<IWorkflowProcess, WorkflowProcess>();
            container.RegisterType<IRoleProcess, RoleProcess>();
            container.RegisterType<IUserProcess, UserProcess>();
            container.RegisterType<IGroupProcess, GroupProcess>();
            container.RegisterType<ICalendarProcess, CalendarProcess>();
            container.RegisterType<IElmahLogProcess, ElmahLogProcess>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}