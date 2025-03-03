using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Lable;

namespace CSHM.Core.Services.Interfaces
{
    public interface ILableService : IRepository<Lable,LableViewModel>
    {
    }
}
