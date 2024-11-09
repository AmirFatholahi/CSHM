using CSHM.Presentation.Base;
using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IPolicyService : IRepository<Policy, PolicyViewModel>
{
    ResultViewModel<PolicyViewModel> SelectAll(bool? activate, int policyParameterID, string filter = null, int? pageNumber = null, int pageSize = 20);

}