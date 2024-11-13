using CSHM.Api.Extensions;
using CSHM.Widget.Config;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

public class HomeController : Controller
{
    private readonly string _baseURL;

    public HomeController()
    {
       // _baseURL = ConfigWidget.GetConfigValue<string>("PublishedServerAddresses:0:BaseUrl");
    }

    public IActionResult Index()
    {
       // ViewBag.Version = PublicExtension.GetVersion();
       // ViewBag.BaseUrl = _baseURL;

        return View();
    }
}
