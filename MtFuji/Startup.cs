using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MtFuji.Startup))]
namespace MtFuji
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
