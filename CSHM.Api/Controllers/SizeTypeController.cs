using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[Authorize]
[ApiController]

public class SizeTypeController : ControllerBase
{
    private readonly ISizeTypeService _sizeTypeService;
    public SizeTypeController(ISizeTypeService sizeTypeService)
    {
        _sizeTypeService = sizeTypeService;
    }
}
