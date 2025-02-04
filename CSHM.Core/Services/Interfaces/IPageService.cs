using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentations.Login;

namespace CSHM.Core.Services.Interfaces;

public interface IPageService : IRepository<Page, PageViewModel>
{
    public ResultViewModel<PageViewModel> SelectAll(bool? activate, int changeTypeID, int? pageNumber = null, int pageSize = 20);

    HttpResponseMessage ExcelAll();

    PageViewModel GetPage(string pageName);

}