using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]
public class LableController : ControllerBase
{
    private readonly ILableService _lableService;

    public LableController(ILableService lableService)
    {
        _lableService = lableService;
    }
}
