using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstafetaApi.Startup))]
namespace EstafetaApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
