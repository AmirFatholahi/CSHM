using CSHM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[Authorize]
[ApiController]
public class CategoryTypeController : ControllerBase
{
    private readonly ICategoryTypeService _categoryTypeService;
    public CategoryTypeController(ICategoryTypeService categoryTypeService)
    {
        _categoryTypeService = categoryTypeService;
    }
}
