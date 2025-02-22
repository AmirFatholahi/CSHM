using CSHM.Core.Services.Interfaces;

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
}
