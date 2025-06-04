using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}