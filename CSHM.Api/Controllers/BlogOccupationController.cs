using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class BlogOccupationController : ControllerBase
{
    private readonly IBlogOccupationService _blogOccupationService;
    public BlogOccupationController(IBlogOccupationService blogOccupationService)
    {
        _blogOccupationService = blogOccupationService;
    }
}
