using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapMvcAttributeRoutes();
			routes.LowercaseUrls = true;
			routes.MapRoute(name : null, url: "{controller}/{action}");
		}
	}
}