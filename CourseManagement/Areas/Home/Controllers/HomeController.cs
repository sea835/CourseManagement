using Microsoft.AspNetCore.Mvc;

[Area("Home")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}