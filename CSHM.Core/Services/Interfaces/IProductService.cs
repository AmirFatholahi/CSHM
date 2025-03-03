using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;

namespace CSHM.Core.Services.Interfaces;

public interface IProductService : IRepository<Product, ProductViewModel>
{
}
