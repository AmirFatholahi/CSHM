using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]
public class BlogTypeController : ControllerBase
{
    private readonly IBlogTypeService _blogTypeService;
    public BlogTypeController(IBlogTypeService blogTypeService)
    {
        _blogTypeService = blogTypeService;
    }
}
