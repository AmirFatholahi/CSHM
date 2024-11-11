using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]
public class PublisherBranchController : ControllerBase
{
    private readonly IPublisherBranchService _publisherBranchService;

    public PublisherBranchController(IPublisherBranchService publisherBranchService)
    {
        _publisherBranchService = publisherBranchService;
    }
}
