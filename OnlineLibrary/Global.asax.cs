using System.Web.Routing;
using OnlineLibrary.App_Start;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); 
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}