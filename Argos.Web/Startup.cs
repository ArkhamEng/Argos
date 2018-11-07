using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Argos.Web.Startup))]
namespace Argos.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
