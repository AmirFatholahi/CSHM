using CSHM.Api.Extensions;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentations.Login;
using CSHM.Widget.Client;
using CSHM.Widget.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class SandBoxController : ControllerBase
{
    private readonly IUserHandler _userHandler;
    private readonly IClientWidget _client;

    public SandBoxController(IUserHandler userHandler , IClientWidget client)
    {
        _userHandler = userHandler;
        _client = client;
    }


    // ============================================================================================ AUTHENTICATION
    [HttpPost]
    [AllowAnonymous]
    [Route("api/sandbox/submit")]
    public IActionResult submit([FromBody] SubmitViewModel entity)
    {
        LoginViewModel model = new LoginViewModel()
        {
            Username = entity.Username,
            Password = entity.Password,
            ApiKey = entity.ApiKey
        };

        var publishedServerAddresses = PublicExtension.GetConfigSection("PublishedServerAddresses");
        var serverAddress = publishedServerAddresses?.Get<List<ServerAddressViewModel>>()?.Select(x => x.Address).ToList();
        var token = _userHandler.Login(model, serverAddress?[0], true, _client.ClientIP());
        if (token == null) return Unauthorized();
        if (token.Status == false && token.Token == String.Empty) return UnprocessableEntity();
        return Ok(token);
    }

}
