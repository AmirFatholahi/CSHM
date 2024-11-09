using CSHM.Presentation.Base;
using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IUserInRoleService : IRepository<UserInRole, UserInRoleViewModel>
{
    MessageViewModel AddOrUpdate(UserInRoleViewModel entity, int creatorID);

    MessageViewModel Activate(int id, int creatorID);

    MessageViewModel AddRoleBase(int userID, int creatorID);

    MessageViewModel AddRole(int userID, int roleID, int creatorID);

    ResultViewModel<UserInRoleViewModel> SelectRolesByUser(bool? activate, int userID);

}
