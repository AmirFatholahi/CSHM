using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Widget.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using CSHM.Widget.Calendar;
using CSHM.Widget.Redis;

namespace CSHM.Api.Extensions;

public class DynamicAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IUserHandler _userHandler;
    private readonly ILogWidget _logWidget;
    private readonly IRedisWidget _redis;

    public DynamicAuthorizationFilter(IUserHandler userHandler, ILogWidget logWidget, IRedisWidget redis)
    {
        _userHandler = userHandler;
        _logWidget = logWidget;
        _redis = redis;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!IsProtectedAction(context))
            return;

        if (!IsUserAuthenticated(context))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userName = _userHandler.GetUserClaims(context.HttpContext.User, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault();

        var currentRequest = GetRequestedControllerAction(context);
        var requestArray = currentRequest.Split(':');
        var controllerName = requestArray[0];
        var actionName = requestArray[1];

        if (!IsTokenValid(context, controllerName, actionName))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (controllerName.ToLower() == "gateway" && actionName.ToLower() == "payment" )
        {
            if (_userHandler.GetUserClaims(context.HttpContext.User, "Step").FirstOrDefault() != "0")
            {
                context.Result = new ForbiddenPage();
            }
        }

        else if (controllerName.ToLower() == "authentication" && (actionName.ToLower() == "twofactor" || actionName.ToLower() == "sendloginotp" || actionName.ToLower() == "getcellphonenumberofuser" || actionName.ToLower() == "changeforce"))
        {
            if (_userHandler.GetUserClaims(context.HttpContext.User, "Step").FirstOrDefault() != "1")
            {
                context.Result = new ForbiddenPage();
            }
        }
        else
        {
            if (_userHandler.GetUserClaims(context.HttpContext.User, "Step").FirstOrDefault() != "2")
            {
                context.Result = new ForbiddenPage();
            }

        }

        //var hasAccess = _userHandler.GetControllerActions(context.HttpContext.User, x => x.ControllerName.ToLower() == controllerName.ToLower() && x.ActionName.ToLower() == actionName.ToLower());


        //if (hasAccess.Any())
        //{
        //    return;
        //}
        //else
        //{
        //    context.Result = new ForbiddenPage();
        //}

    }

    private bool IsProtectedAction(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            return false;
        try
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            return false;
        }
        catch (Exception ex)
        {
            _logWidget.ExceptionLog(ex, "IsProtectedAction In DynamicAuthorizationFilter");

            return true;
        }
    }

    private bool IsUserAuthenticated(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTokenValid(AuthorizationFilterContext context, string controllerName, string actionName)
    {
        var tokenType = _userHandler.GetUserClaims(context.HttpContext.User, "TokenType").FirstOrDefault() ?? "";
        var expireDate = _userHandler.GetUserClaims(context.HttpContext.User, "Expire").FirstOrDefault() ?? "";
        var jti = _userHandler.GetUserClaims(context.HttpContext.User, "jti").FirstOrDefault() ?? "";
        var exist = _redis.IsExistForbiddenToken(jti);
        if (exist == true)
        {
            return false;
        }

        if (controllerName.ToLower() == "authentication" && actionName.ToLower() == "getrefreshtoken")
        {
            if (tokenType == "refresh-token")
            {
                if (string.IsNullOrWhiteSpace(expireDate) || CalenderWidget.ToGregDateTime(expireDate) < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    var firstLogin = _userHandler.GetUserClaims(context.HttpContext.User, "FirstLogin").FirstOrDefault() ?? "";
                    if (Convert.ToDateTime(firstLogin).AddMonths(1) < DateTime.Now)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(expireDate) && CalenderWidget.ToGregDateTime(expireDate) < DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    private string GetRequestedControllerAction(AuthorizationFilterContext context)
    {
        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
        var controller = controllerActionDescriptor.ControllerName.ToLower();
        var action = controllerActionDescriptor.ActionName.ToLower();
        return $"{controller}:{action}";
    }
}