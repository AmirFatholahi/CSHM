using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using CSHM.Presentation.Publish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet]
    [Route("api/publisher/getAll/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAll(bool? activate,int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productService.SelectAllByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }


}
