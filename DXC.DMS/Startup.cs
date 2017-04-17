using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DXC.DMS.Startup))]
namespace DXC.DMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
