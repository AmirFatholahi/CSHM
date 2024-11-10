using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]
public class LableController : ControllerBase
{
    private readonly ILableService _lableService;

    public LableController(ILableService lableService)
    {
        _lableService = lableService;
    }
}
