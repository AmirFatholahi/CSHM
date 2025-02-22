using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]

public class PersonOccupationController : ControllerBase
{
    private readonly IPersonOccupationService _personOccupationService;

    public PersonOccupationController(IPersonOccupationService personOccupationService)
    {
        _personOccupationService = personOccupationService;
    }
}
