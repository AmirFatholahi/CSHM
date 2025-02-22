using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class BlogOccupationController : ControllerBase
{
    private readonly IBlogOccupationService _blogOccupationService;
    public BlogOccupationController(IBlogOccupationService blogOccupationService)
    {
        _blogOccupationService = blogOccupationService;
    }
}
