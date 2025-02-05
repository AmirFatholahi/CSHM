using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Publish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]

public class PublisherController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    [Route("api/publisher/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<PublisherViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null )
    {
        var result = _publisherService.SelectAll(activate,filter,pageNumber,pageSize);
        return result;
    }

}
