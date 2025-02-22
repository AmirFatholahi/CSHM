using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]
public class BlogTypeController : ControllerBase
{
    private readonly IBlogTypeService _blogTypeService;
    public BlogTypeController(IBlogTypeService blogTypeService)
    {
        _blogTypeService = blogTypeService;
    }
}
