using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]
public class OccupationController : ControllerBase
{
    private readonly IOccupationService _occupationService;
    public OccupationController(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }
}
