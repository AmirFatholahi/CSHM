using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;
[ApiController]

public class ProductPropertyTypeController : ControllerBase
{
    private readonly IProductPropertyTypeService _productPropertyTypeService;
    public ProductPropertyTypeController(IProductPropertyTypeService productPropertyTypeService)
    {
        _productPropertyTypeService = productPropertyTypeService;
    }

    [HttpGet]
    [Route("api/productPropertyType/getAllByProductID/{productID}/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductPropertyTypeViewModel> getAllByMerchantID(int productID, bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productPropertyTypeService.SelectAllByProductID(productID, activate, filter, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/productPropertyType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductPropertyTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productPropertyTypeService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }
}
