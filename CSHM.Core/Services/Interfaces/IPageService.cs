using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentations.Login;

namespace CSHM.Core.Services.Interfaces;

public interface IPageService : IRepository<Page, PageViewModel>
{
    HttpResponseMessage ExcelAll();

    PageViewModel GetPage(string pageName);

}