using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class ProductCommentController : ControllerBase
{

    private readonly IProductCommentService _productCommentService;
    public ProductCommentController(IProductCommentService productCommentService)
    {
        _productCommentService = productCommentService;
    }
}
