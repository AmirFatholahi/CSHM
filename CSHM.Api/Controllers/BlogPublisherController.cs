using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class BlogPublisherController : ControllerBase
{
    private readonly IBlogPublisherService _blogPublisherService;
    public BlogPublisherController(IBlogPublisherService blogPublisherService)
    {
        _blogPublisherService = blogPublisherService;   
    }
}
