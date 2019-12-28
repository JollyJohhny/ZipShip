using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZipShip.Startup))]
namespace ZipShip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
