using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GkWebApp.Startup))]
namespace GkWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
