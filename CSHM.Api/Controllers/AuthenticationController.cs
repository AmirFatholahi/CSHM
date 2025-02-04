using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSHM.Api.Extensions;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.OTP;
using CSHM.Presentations.Login;
using CSHM.Presentations.User;
using CSHM.Widget.Client;
using CSHM.Widget.Redis;
using CSHM.Widget.Rest;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IUserHandler _userHandler;
    private readonly ISettingService _settingService;
    private readonly IRedisWidget _redis;
    private readonly IClientWidget _client;
    private readonly INotificationHandler _notificationHandler;

    public AuthenticationController(IUserHandler userHandler, ISettingService settingService, IRedisWidget redis, IClientWidget client, INotificationHandler notificationHandler)
    {
        _userHandler = userHandler;
        _settingService = settingService;
        _redis = redis;
        _client = client;
        _notificationHandler = notificationHandler;
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("api/authentication/login")]
    public IActionResult Login([FromBody] LoginViewModel model)
    {
        var publishedServerAddresses = PublicExtension.GetConfigSection("PublishedServerAddresses");
        var serverAddress = publishedServerAddresses?.Get<List<ServerAddressViewModel>>()?.Select(x => x.Address).ToList();
        var token = _userHandler.Login(model, serverAddress?[0], false, _client.ClientIP());
        if (token == null) return Unauthorized();
        if (token.Status == false && token.Token == String.Empty) return UnprocessableEntity();
        return Ok(token);
    }

    [HttpGet]
    [Authorize]
    [Route("api/authentication/logout")]
    public MessageViewModel logout()
    {
        var result = _userHandler.Logout(User.GetUserID(), _client.ClientIP());
        return result;
    }


    //[HttpPost]
    //[Authorize]
    //[Route("api/authentication/twoFactor")]
    //public TokenViewModel twoFactor([FromBody] OTPViewModel entity)
    //{
    //    var publishedServerAddresses = PublicExtension.GetConfigSection("PublishedServerAddresses");
    //    var serverAddress = publishedServerAddresses?.Get<List<ServerAddressViewModel>>()?.Select(x => x.Address).ToList();
    //    var result = _userHandler.TwoFactor(entity, serverAddress?[0], User.GetUserClaims("Step"), _client.ClientIP(), User.GetJTI());
    //    return result;
    //}


    [HttpGet]
    [Authorize]
    [Route("api/authentication/getRefreshToken")]
    public TokenViewModel getRefreshToken()
    {
        var publishedServerAddresses = PublicExtension.GetConfigSection("PublishedServerAddresses");
        var serverAddress = publishedServerAddresses?.Get<List<ServerAddressViewModel>>()?.Select(x => x.Address).ToList();
        var result = _userHandler.GetRefreshToken(2, serverAddress?[0], User.GetUserID(), User.GetJTI(), User.GetUserFirstLogin());
        return result;
    }

    [HttpPost]
    [Authorize]
    [Route("api/authentication/sendLoginOTP")]
    public MessageViewModel sendLoginOTP([FromBody] OTPViewModel entity)
    {
        entity.UserID = User.GetUserID();
        var result = _notificationHandler.SendOTP(entity);
        return result;
    }


    [HttpGet]
    [Authorize]
    [Route("api/authentication/getCellphoneNumberOfUser")]
    public MessageViewModel getCellphoneNumberOfUser()
    {
        var result = _userHandler.SelectCellphoneOfUser(User.GetUserID());
        return result;
    }



    //[HttpPost]
    //[AllowAnonymous]
    //[Route("api/authentication/resetPassword")]
    //public MessageViewModel resetPassword([FromBody] ResetPasswordViewModel entity)
    //{
    //    var result = _userHandler.ResetPassword(entity, false, true, false, true, User.GetUserID(), _client.ClientIP());
    //    return result;
    //}

    //[HttpPost]
    //[Authorize]
    //[Route("api/authentication/changeForce")]
    //public MessageViewModel changeForce([FromBody] ResetPasswordViewModel entity)
    //{
    //    entity.UserID = User.GetUserID();
    //    var result = _userHandler.ResetPassword(entity, false, false, false, false, User.GetUserID(), _client.ClientIP());
    //    return result;
    //}

    //[HttpGet]
    //[AllowAnonymous]
    //[Route("api/authentication/getCaptcha/{sessionID}")]
    //public List<string> GetCaptcha(string sessionID)
    //{
    //    var result = _redis.CreateCaptcha(sessionID);
    //    result.Add(PublicExtension.GetVersion());
    //    return result;
    //}


    [HttpGet]
    [Authorize]
    [Route("api/authentication/getPages")]
    public List<PageViewModel> GetPages()
    {
        var result = _userHandler.GetPages(User.GetUserID());
        return result;
    }


    [HttpGet]
    [Authorize]
    [Route("api/authentication/getNavigation")]
    public List<MenuViewModel> GetNavigation()
    {
        var result = _userHandler.GetMenus(User.GetUserID());
        return result;
    }


    //[HttpGet]
    //[Route("api/authentication/getProfile")]
    //public ProfileViewModel GetProfile()
    //{
    //    var result = _userHandler.GetProfile(User.GetUserID());
    //    return result;
    //}
}
