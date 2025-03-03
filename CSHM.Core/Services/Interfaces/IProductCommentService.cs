using CSHM.Presentation.Base;
using CSHM.Presentation.Product;

namespace CSHM.Core.Services.Interfaces
{
    public interface IProductCommentService
    {
        public ResultViewModel<ProductCommentViewModel> SelectAllByProductID(bool? activate, int productID, int? pageNumber = null, int pageSize = 20);
    }
}
