using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppointmentSetter.Startup))]
namespace AppointmentSetter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
