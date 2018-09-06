using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrivateLabelLite.Startup))]
namespace PrivateLabelLite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
