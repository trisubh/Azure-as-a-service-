using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Search_as_a_service_azure.Startup))]
namespace Search_as_a_service_azure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
