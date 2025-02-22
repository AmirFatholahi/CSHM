using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;


namespace CSHM.Core.Services.Interfaces
{
    public interface IProductPropertyTypeService : IRepository<ProductPropertyType, ProductPropertyTypeViewModel>
    {
        public ResultViewModel<ProductPropertyTypeViewModel> SelectAllByProductID(int productID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);
    }
}
