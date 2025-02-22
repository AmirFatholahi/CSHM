using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]
public class MediaTypeController : ControllerBase
{
    private readonly IMediaTypeService _mediaTypeService;
    public MediaTypeController(IMediaTypeService mediaTypeService)
    {
        _mediaTypeService = mediaTypeService;
    }
}
