using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;

namespace CSHM.Core.Services.Interfaces;

public interface IUserService : IRepository<User, UserViewModel>
{

    User CheckUser(string username, string password);

    ResultViewModel<UserViewModel> SelectAllByUserType(int userTypeID, bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20);

    MessageViewModel GenerateUser(User entity, int creatorID, string ip); 

    HttpResponseMessage ExcelAll();

    List<ErrorViewModel> ValidationForm(UserViewModel entity);

    string GenerateRandomPassword();

    UserViewModel SelectUserByNID(string nid);

    MessageViewModel UserActivate(int id, int creatorID);
}