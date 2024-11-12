using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class ProductPublisherController : ControllerBase
{
    private readonly IProductPublisherService _productPublisherService;
    public ProductPublisherController(IProductPublisherService productPublisherService)
    {
        _productPublisherService = productPublisherService;
    }
}
