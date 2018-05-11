using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineBookLibrary.Startup))]
namespace OnlineBookLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
