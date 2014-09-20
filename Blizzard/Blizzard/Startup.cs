using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blizzard.Startup))]
namespace Blizzard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
