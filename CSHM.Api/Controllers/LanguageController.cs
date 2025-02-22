using CSHM.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;


[ApiController]

public class LanguageController : ControllerBase
{
    private readonly ILanguageService _languageService;
    public LanguageController(ILanguageService languageService)
    {
        _languageService = languageService;
    }
}
