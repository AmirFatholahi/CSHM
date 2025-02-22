
using Microsoft.AspNetCore.Mvc;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Core.Services.Interfaces;
using CSHM.Presentations.Login;
using Microsoft.AspNetCore.Authorization;


namespace CSHM.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;


        public PageController(IPageService pageService)
        {

            _pageService = pageService;
        }

        // ========================================================================================
        // ******************************************************************************************
        // ******************************************************************************************
        // ****************************************************************************************** Menu

        [HttpGet]
        [Route("api/page/getAllMenus/{channelTypeID}")]
        public ResultViewModel<PageViewModel> getAllMenus(int channelTypeID)
        {
            var result = _pageService.SelectAllBychangeTypeID(true,channelTypeID);
            return result;
        }


    }
}
