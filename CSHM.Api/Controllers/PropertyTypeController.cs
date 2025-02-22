using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[ApiController]

public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeService _propertyTypeService;
    public PropertyTypeController(IPropertyTypeService propertyTypeService )
    {
        _propertyTypeService = propertyTypeService;
    }


    [HttpGet]
    [Route("api/propertyType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<PropertyTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _propertyTypeService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }
}
