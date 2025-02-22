using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]

public class PersonTypeController : ControllerBase
{
    private readonly IPersonTypeService _personTypeService;

    public PersonTypeController(IPersonTypeService personTypeService)
    {
        _personTypeService = personTypeService;
    }
}
