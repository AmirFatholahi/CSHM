using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet]
    [Route("api/blog/getAllPinByBlogTypeIDAndPublisherID/{publisherID}/{blogTypeID}/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<BlogViewModel> getAllPinByBlogTypeIDAndPublisherID(int publisherID, int blogTypeID, bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _blogService.SelectAllPinByBlogTypeIDAndPublisherID(publisherID, blogTypeID, activate, filter, pageNumber, pageSize);
        return result;
    }


    [HttpGet]
    [Route("api/blog/getAllByBlogTypeIDAndPublisherID/{publisherID}/{blogTypeID}/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<BlogViewModel> getAllByBlogTypeIDAndPublisherID(int publisherID,int blogTypeID, bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _blogService.SelectAllByBlogTypeIDAndPublisherID(publisherID, blogTypeID, activate, filter, pageNumber, pageSize);
        return result;
    }


    [HttpGet]
    [Route("api/blog/getAllByPublisherID/{publisherID}/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<BlogViewModel> getAllByPublisherID(int publisherID, bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _blogService.SelectAllByPublisherID(publisherID, activate, filter, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/blog/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<BlogViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _blogService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }

}
