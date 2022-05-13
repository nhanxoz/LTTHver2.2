using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LTTHver2._2.Startup))]
namespace LTTHver2._2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
