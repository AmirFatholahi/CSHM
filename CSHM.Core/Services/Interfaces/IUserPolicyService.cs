using CSHM.Presentation.Base;
using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IUserPolicyService : IRepository<UserPolicy, UserPolicyViewModel>
{
    MessageViewModel AddOrUpdate(UserPolicyViewModel entity, int creatorID);

    MessageViewModel Activate(int id, int creatorID);

    ResultViewModel<UserPolicyViewModel> SelectAllUserPolicies(bool? activate, int userID);

    ResultViewModel<UserPolicyViewModel> SelectAllUserPolicies(bool? activate, int userID, int policyParameterID);

    ResultViewModel<UserPolicyViewModel> SelectAllByUser(int userID, bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20);



}