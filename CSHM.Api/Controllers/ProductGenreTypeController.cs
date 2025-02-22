using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[ApiController]


    public class ProductGenreTypeController : ControllerBase
    {
        private readonly IProductGenreTypeService _productGenreTypeService;
        public ProductGenreTypeController(IProductGenreTypeService productGenreTypeService)
        {
            _productGenreTypeService = productGenreTypeService;
        }

        [HttpGet]
        [Route("api/productGenreType/getAllByProductID/{productID}/{activate?}/{pageNumber?}/{pageSize?}")]
        public ResultViewModel<ProductGenreTypeViewModel> getAllByMerchantID(int productID, bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
        {
            var result = _productGenreTypeService.SelectAllByProductID(productID, activate, filter, pageNumber, pageSize);
            return result;
        }

        [HttpGet]
        [Route("api/productGenreType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
        public ResultViewModel<ProductGenreTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
        {
            var result = _productGenreTypeService.SelectAll(activate, filter, pageNumber, pageSize);
            return result;
        }
    }
