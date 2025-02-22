using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService _productTypeService;

    public ProductTypeController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;

    }
}

