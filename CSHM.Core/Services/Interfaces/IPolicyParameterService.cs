using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IPolicyParameterService : IRepository<PolicyParameter, PolicyParameterViewModel>
{
    List<PolicyParameterViewModel> SelectAllBySide(bool? activate, string side);

}