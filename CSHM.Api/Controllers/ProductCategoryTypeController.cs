using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class ProductCategoryTypeController : ControllerBase
{
    private readonly IProductCategoryTypeService _productCategoryTypeService;
    public ProductCategoryTypeController(IProductCategoryTypeService productCategoryTypeService)
    {
        _productCategoryTypeService = productCategoryTypeService;
    }
}
