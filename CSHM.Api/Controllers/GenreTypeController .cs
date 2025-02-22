using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

[ApiController]

public class GenreTypeController : ControllerBase
{
    private readonly IGenreTypeService _genreTypeService;
    public GenreTypeController(IGenreTypeService genreTypeService )
    {
        _genreTypeService = genreTypeService;
    }


    [HttpGet]
    [Route("api/genreType/getAll/{activate?}/{pageNumber?}/{pageSize?}")]
    public ResultViewModel<GenreTypeViewModel> getAll(bool? activate, int? pageNumber = null, int pageSize = 20, string? filter = null)
    {
        var result = _genreTypeService.SelectAll(activate, filter, pageNumber, pageSize);
        return result;
    }
}
