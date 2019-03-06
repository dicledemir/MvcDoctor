using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcDoctor.Startup))]
namespace MvcDoctor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
