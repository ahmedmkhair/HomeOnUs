using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeOnUs.Startup))]
namespace HomeOnUs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
