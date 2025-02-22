using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class ProductOccupationController : ControllerBase
{
    private readonly IProductOccupationService _productOccupationService;
    public ProductOccupationController(IProductOccupationService productOccupationService)
    {
        _productOccupationService = productOccupationService;
    }
}
