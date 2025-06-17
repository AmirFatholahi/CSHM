using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Category;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]
public class CategoryTypeController : ControllerBase
{
    private readonly ICategoryTypeService _categoryTypeService;
    public CategoryTypeController(ICategoryTypeService categoryTypeService)
    {
        _categoryTypeService = categoryTypeService;
    }

    [HttpGet]
    [Route("api/categoryType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<CategoryTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _categoryTypeService.SelectAll(activate,filter, pageNumber, pageSize);
        return result;
    }


}
