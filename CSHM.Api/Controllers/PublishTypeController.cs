using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]

public class PublishTypeController : ControllerBase
{
    private readonly IPublishTypeService _publishTypeService;

    public PublishTypeController(IPublishTypeService publishTypeService)
    {
        _publishTypeService = publishTypeService;
        
    }
}

