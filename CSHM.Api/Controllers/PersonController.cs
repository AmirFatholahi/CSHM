using CSHM.Api.Extensions;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.People;
using CSHM.Widget.Ticketum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]

public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IPersonHandler _personHandler;


    public PersonController(IPersonService personService,IPersonHandler personHandler)
    {
        _personService = personService;
        _personHandler = personHandler;
    }


    [HttpGet]
    [Route("api/person/getAllByOccupation/{occupationID}")]
    public ResultViewModel<PersonViewModel> getAllByOccupation(int occupationID)
    {
        var result = _personHandler.SelectAllByOccupation(occupationID);
        return result;
    }

}
