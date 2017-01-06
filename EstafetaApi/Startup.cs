using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EstafetaApi.Startup))]
namespace EstafetaApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
