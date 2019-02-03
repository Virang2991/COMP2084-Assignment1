using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodOnFinger.Startup))]
namespace FoodOnFinger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
