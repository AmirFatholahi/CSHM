using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class ProductLableController : ControllerBase
{
    private readonly IProductLableService _productLableService;

    public ProductLableController(IProductLableService productLableService)
    {
        _productLableService = productLableService;
    }
}
