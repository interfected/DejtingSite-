using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DejtingSite_.Startup))]
namespace DejtingSite_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
