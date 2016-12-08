using System.Web.Mvc;

namespace AppointmentSetter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}