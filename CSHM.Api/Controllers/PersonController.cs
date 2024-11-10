using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace POS.Api.Controllers;

[Authorize]
[ApiController]

public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;


    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }


}
