using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;
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

    [HttpGet]
    [Route("api/blogType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<BlogTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _blogTypeService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }

}
