using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class ProductLableController : ControllerBase
{
    private readonly IProductLableService _productLableService;

    public ProductLableController(IProductLableService productLableService)
    {
        _productLableService = productLableService;
    }
}
