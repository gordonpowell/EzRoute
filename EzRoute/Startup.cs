using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EzRoute.Startup))]
namespace EzRoute
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
