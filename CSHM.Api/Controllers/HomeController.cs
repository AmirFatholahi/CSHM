using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
