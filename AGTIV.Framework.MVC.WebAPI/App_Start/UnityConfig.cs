using AGTIV.Framework.MVC.Business.Authentication;
using AGTIV.Framework.MVC.Business.ElmahLog;
using AGTIV.Framework.MVC.Business.Maintenance;
using AGTIV.Framework.MVC.Business.Role;
using AGTIV.Framework.MVC.Business.RunningNumbers;
using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Business.User;
using AGTIV.Framework.MVC.Business.Workflows;
using AGTIV.Framework.MVC.Data.Context;
using AGTIV.Framework.MVC.Data.Repositories;
using AGTIV.Framework.MVC.Data.UnitOfWork;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Interfaces;
using AGTIV.Framework.MVC.WebAPI.Providers;
using Microsoft.Owin.Security.OAuth;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using AGTIV.Framework.MVC.Framework.Authentication;

namespace AGTIV.Framework.MVC.WebAPI
{
    public static class UnityConfig
    {
        public static IUnityContainer container;

        public static void RegisterComponents()
        {
            container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAppSetting, AppSetting>();
            container.RegisterType<DbContext, AppDbContext>();
            container.RegisterType<IAppUserStore, AppUserStore>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<IWorkflowRepository, WorkflowRepository>();

            container.RegisterType<IAuthenticationComponent, AuthenticationComponent>();
            container.RegisterType<IWorkflowComponent, WorkflowComponent>();
            container.RegisterType<IRoleComponent, RoleComponent>();
            container.RegisterType<IUserComponent, UserComponent>();
            container.RegisterType<IGroupComponent, GroupComponent>();
            container.RegisterType<ICalendarComponent, CalendarComponent>();
            container.RegisterType<IElmahComponent, ElmahComponent>();
            container.RegisterType<IRunningNumberComponent, RunningNumberComponent>();

            container.RegisterType<OAuthAuthorizationServerProvider, ApplicationOAuthProvider>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}