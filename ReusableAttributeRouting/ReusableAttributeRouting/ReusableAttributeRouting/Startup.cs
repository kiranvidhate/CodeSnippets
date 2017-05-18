using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReusableAttributeRouting.Startup))]
namespace ReusableAttributeRouting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
