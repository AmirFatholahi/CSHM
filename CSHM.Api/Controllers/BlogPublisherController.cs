using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class BlogPublisherController : ControllerBase
{
    private readonly IBlogPublisherService _blogPublisherService;
    public BlogPublisherController(IBlogPublisherService blogPublisherService)
    {
        _blogPublisherService = blogPublisherService;   
    }
}
