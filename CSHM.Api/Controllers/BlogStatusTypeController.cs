using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]
public class BlogStatusTypeController : ControllerBase
{
    private readonly IBlogStatusTypeService _blogStatusTypeService;

    public BlogStatusTypeController(IBlogStatusTypeService blogStatusTypeService)
    {
        _blogStatusTypeService = blogStatusTypeService;
    }
}
