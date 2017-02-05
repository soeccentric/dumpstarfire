using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using WebApplication1.App_Start;
using WebApplication1.Services;

namespace WebApplication1
{
    public class MvcApplication : HttpApplication
    {
        private IMessageSource MessageSource()
        {
            return (IMessageSource) UnityConfig.GetConfiguredContainer().Resolve(typeof(IMessageSource));
        }

        protected void Application_Start()
        {
            MessageSource().Initialize();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            MessageSource().Shutdown();
        }
    }
}
