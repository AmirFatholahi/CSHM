using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class ProductCategoryTypeController : ControllerBase
{
    private readonly IProductCategoryTypeService _productCategoryTypeService;
    public ProductCategoryTypeController(IProductCategoryTypeService productCategoryTypeService)
    {
        _productCategoryTypeService = productCategoryTypeService;
    }
}
