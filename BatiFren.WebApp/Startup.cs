using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BatiFren.WebApp.Startup))]
namespace BatiFren.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}