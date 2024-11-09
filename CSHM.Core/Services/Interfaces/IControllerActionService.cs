using CSHM.Presentations;
using CSHM.Presentations.Login;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IControllerActionService : IRepository<ControllerAction, ControllerActionViewModel>
{
    HttpResponseMessage ExcelAll();

}