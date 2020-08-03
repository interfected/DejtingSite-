using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DejtingSidan.Startup))]
namespace DejtingSidan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
