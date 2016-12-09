using System.Web.Mvc;

namespace AppointmentSetter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Appointment");
        }
    }
}