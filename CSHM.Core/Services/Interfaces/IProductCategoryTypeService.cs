using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Product;

namespace CSHM.Core.Services.Interfaces
{
    public interface IProductCategoryTypeService : IRepository<ProductCategoryType,ProductCategoryTypeViewModel>
    {
    }
}
