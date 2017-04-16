using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BotChallenge.Startup))]
namespace BotChallenge
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}