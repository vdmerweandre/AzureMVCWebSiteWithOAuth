using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureMVCWebSiteWithOAuth.Startup))]
namespace AzureMVCWebSiteWithOAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
