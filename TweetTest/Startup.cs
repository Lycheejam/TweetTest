using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TweetTest.Startup))]
namespace TweetTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
