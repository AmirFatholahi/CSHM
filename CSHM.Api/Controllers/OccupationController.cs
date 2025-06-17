using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Occupation;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]
public class OccupationController : ControllerBase
{
    private readonly IOccupationService _occupationService;
    public OccupationController(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    [HttpGet]
    [Route("api/occupation/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<OccupationViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _occupationService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }


}
