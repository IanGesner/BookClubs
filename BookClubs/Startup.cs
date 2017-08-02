using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookClubs.Startup))]
namespace BookClubs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
