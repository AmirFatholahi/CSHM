using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]
public class MediaTypeController : ControllerBase
{
    private readonly IMediaTypeService _mediaTypeService;
    public MediaTypeController(IMediaTypeService mediaTypeService)
    {
        _mediaTypeService = mediaTypeService;
    }
}
