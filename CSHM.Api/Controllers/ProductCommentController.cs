using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class ProductCommentController : ControllerBase
{

    private readonly IProductCommentService _productCommentService;
    public ProductCommentController(IProductCommentService productCommentService)
    {
        _productCommentService = productCommentService;
    }

    [HttpGet]
    [Route("api/productComment/getAllByProductID/{activate?}/{productID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductCommentViewModel> getAllByProductID(bool? activate, int productID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productCommentService.SelectAllByProductID(activate, productID, pageNumber, pageSize);
        return result;
    }
}
