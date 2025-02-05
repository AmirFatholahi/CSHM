using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;

namespace CSHM.Core.Services.Interfaces;

public interface IProductService : IRepository<Product, ProductViewModel>
{
    public ResultViewModel<ProductViewModel> SelectAllByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

}
