using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace AppointmentSetter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Injection
            //var container = new Container();
            //container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //// Register your types, for instance:
            ////var context = new ApplicationDbContext();
            ////container.Register<DbContext, ApplicationDbContext>(Lifestyle.Scoped);
            ////container.Register<IApplicationUserRepository, ApplicationUserRepository>(Lifestyle.Scoped);
            //container.Register<IAppointmentAttenderRepository, AppointmentAttenderRepository>(Lifestyle.Scoped);
            //container.Register<IAppointmentRepository, AppointmentRepository>(Lifestyle.Scoped);
            //container.Register<IAppointmentTypeRepository, AppointmentTypeRepository>(Lifestyle.Scoped);
            
            //// This is an extension method from the integration package.
            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.Verify();

            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
