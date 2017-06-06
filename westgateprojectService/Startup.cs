using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(westgateprojectService.Startup))]

namespace westgateprojectService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}