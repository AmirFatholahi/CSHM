using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.People;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


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
    [Route("api/person/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<PersonViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20)
    {
        var result = _personHandler.SelectAll(activate, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/person/getAllPin/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<PersonViewModel> getAllPin(bool? activate, int? pageNumber = null, int pageSize = 20)
    {
        var result = _personHandler.SelectAllPin(activate, pageNumber, pageSize);
        return result;
    }

    [HttpGet]
    [Route("api/person/getBypersonID/{activate?}/{personID}/{pageNumber?}/{pageSize?}")]
    public PersonViewModel getBypersonID(bool? activate,int personID ,int? pageNumber = null, int pageSize = 20)
    {
        var result = _personService.SelectByID(personID);
        return result;
    }


    [HttpGet]
    [Route("api/person/getAllByOccupation/{occupationID}")]
    public ResultViewModel<PersonViewModel> getAllByOccupation(int occupationID)
    {
        var result = _personHandler.SelectAllByOccupation(occupationID);
        return result;
    }

}
