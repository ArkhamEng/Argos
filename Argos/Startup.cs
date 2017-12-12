using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Argos.Startup))]
namespace Argos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
