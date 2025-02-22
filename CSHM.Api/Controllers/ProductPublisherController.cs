using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class ProductPublisherController : ControllerBase
{
    private readonly IProductPublisherService _productPublisherService;
    public ProductPublisherController(IProductPublisherService productPublisherService)
    {
        _productPublisherService = productPublisherService;
    }
}
