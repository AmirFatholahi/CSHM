using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]

public class PersonOccupationController : ControllerBase
{
    private readonly IPersonOccupationService _personOccupationService;

    public PersonOccupationController(IPersonOccupationService personOccupationService)
    {
        _personOccupationService = personOccupationService;
    }
}
