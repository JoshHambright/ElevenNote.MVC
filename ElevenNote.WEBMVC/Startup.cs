using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElevenNote.WEBMVC.Startup))]
namespace ElevenNote.WEBMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
