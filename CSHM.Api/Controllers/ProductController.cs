using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Lable;
using CSHM.Presentation.Product;


using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductHandler _productHandler;
    public ProductController(IProductService productService,IProductHandler productHandler)
    {
        _productService = productService;
        _productHandler = productHandler;
    }

    [HttpGet]
    [Route("api/product/getAll/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAll(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }


    [HttpGet]
    [Route("api/product/getAllNew/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAllNew(bool? activate,int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllNewByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }


    [HttpGet]
    [Route("api/product/getAllRecommended/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAllRecommended(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllRecommendedByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/product/getAllSelected/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAllSelected(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllSelectedByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/product/getAllSoon/{activate?}/{publisherID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAllSoon(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllSoonByPublisher(activate, publisherID, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/product/getAllLableByProductID/{activate?}/{productID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<LableViewModel> getAllLableByProductID(bool? activate, int productID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllLableByProductID(activate, productID, pageNumber, pageSize);
        return result;
    }


    [HttpGet]
    [Route("api/product/getAllByCategoryType/{activate?}/{categoryTypeID}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<ProductViewModel> getAllByCategoryType(bool? activate, int categoryTypeID, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _productHandler.SelectAllByCategoryType(activate, categoryTypeID, pageNumber, pageSize);
        return result;
    }



}
