using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]
public class BlogStatusTypeController : ControllerBase
{
    private readonly IBlogStatusTypeService _blogStatusTypeService;

    public BlogStatusTypeController(IBlogStatusTypeService blogStatusTypeService)
    {
        _blogStatusTypeService = blogStatusTypeService;
    }
}
