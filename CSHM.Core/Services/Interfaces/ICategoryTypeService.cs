using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Category;

namespace CSHM.Core.Services.Interfaces
{
    public interface ICategoryTypeService : IRepository<CategoryType,CategoryTypeViewModel>
    {
    }
}
