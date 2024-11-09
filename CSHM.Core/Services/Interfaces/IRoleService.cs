using CSHM.Presentations.Role;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface IRoleService : IRepository<Role, RoleViewModel>
{
    HttpResponseMessage ExcelAll();

}
