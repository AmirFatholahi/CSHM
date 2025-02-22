using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class CoverTypeController : ControllerBase
{
    private readonly ICoverTypeService _coverTypeService;

    public CoverTypeController(ICoverTypeService coverTypeService)
    {
        _coverTypeService = coverTypeService;
    }
}
