using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.People;

namespace CSHM.Core.Services.Interfaces
{
    public interface IPersonService : IRepository<Person,PersonViewModel>
    {
    }
}
