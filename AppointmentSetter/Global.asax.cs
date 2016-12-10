using AppointmentSetter.DataAccess;
using AppointmentSetter.Service;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
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
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            //var context = new ApplicationDbContext();
            //container.Register<DbContext, ApplicationDbContext>(Lifestyle.Scoped);

            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IAppointmentRepository, AppointmentRepository>(Lifestyle.Scoped);
            container.Register<IAppointmentTypeRepository, AppointmentTypeRepository>(Lifestyle.Scoped);
            container.Register<IConflictChecker, ConflictChecker>(Lifestyle.Scoped);
            container.Register<IDbContext, AppointmentDBContext>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
