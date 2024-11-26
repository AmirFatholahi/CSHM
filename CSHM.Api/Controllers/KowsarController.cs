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
    public MessageViewModel productsedit(GoodViewModel goodViewModel) 
    {
     //   MessageViewModel result = null;

        var result = _kowsarHandler.InsertToGood(goodViewModel);

        return result;

    }



}
