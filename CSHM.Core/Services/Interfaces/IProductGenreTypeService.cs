using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;


namespace CSHM.Core.Services.Interfaces
{
    public interface IProductGenreTypeService : IRepository<ProductGenreType , ProductGenreTypeViewModel>
    {
        public ResultViewModel<ProductGenreTypeViewModel> SelectAllByProductID(int productID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);
    }
}
