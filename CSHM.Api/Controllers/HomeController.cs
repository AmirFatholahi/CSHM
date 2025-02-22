using CSHM.Api.Extensions;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Publish;
using CSHM.Widget.Client;
using CSHM.Widget.Config;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace CSHM.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _baseURL;
        private readonly IClientWidget _client;
        private readonly IPublisherService _publisherService;
        private readonly IProductService _productService;
        private readonly IPageService _pageService;
        private readonly IPersonHandler _personHandler;

        private readonly IProductHandler _productHandler;
        public HomeController(IClientWidget client,IPublisherService publisherService, IProductService productService, IPageService pageService,IPersonHandler personHandler,IProductHandler productHandler)
        {

            _baseURL = ConfigWidget.GetConfigValue<string>("PublishedServerAddresses:0:BaseUrl");
            _client = client;
            _publisherService = publisherService;
            _productService = productService;
            _pageService = pageService;
            _personHandler = personHandler;
            _productHandler = productHandler;
        }

        public IActionResult Index()
        {

            //var x = _publisherService.GetAll(true,null, 1, 20);
            var x = _productHandler.SelectAllByPublisher(true, 1, 1, 20);
            //var x = _pageService.SelectAllBychangeTypeID(true, 1, 1, 20);
           // var x = _personHandler.SelectAllByOccupation(1);
            ViewBag.Version = PublicExtension.GetVersion();
            ViewBag.BaseUrl = _baseURL;
            return View();
        }


    }
}
