using CSHM.Api.Extensions;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Kowsar;
using CSHM.Presentations.Login;
using CSHM.Widget.Client;
using CSHM.Widget.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Controllers;



[ApiController]

public class KowsarController : ControllerBase
{
    private readonly IKowsarHandler _kowsarHandler;

    public KowsarController(IKowsarHandler kowsarHandler)
    {
        _kowsarHandler = kowsarHandler;
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("api/productsedit")]
    public MessageViewModel productsedit(List<GoodViewModel> goodViewModel) 
    {
     //   MessageViewModel result = null;

        var result = _kowsarHandler.InsertToGood(goodViewModel);

        if (result.Status == "Error")
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        }



        return result;

    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/productsdelete")]
    public MessageViewModel productsdelete(List<GoodViewModel> goodViewModel)
    {
        //   MessageViewModel result = null;

        var result = _kowsarHandler.DeleteGood(goodViewModel);

        if (result.Status == "Error")
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        }


        return result;

    }


    [HttpPost]
    [AllowAnonymous]
    [Route("api/common")]
    public MessageViewModel common(List<CommonViewModel> commonViewModel)
    {
        //   MessageViewModel result = null;

        var result = _kowsarHandler.Common(commonViewModel);

        if (result.Status == "Error")
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        }



        return result;

    }

}
