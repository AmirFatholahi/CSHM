using CSHM.Presentation.Base;
using CSHM.Presentation.Lable;
using CSHM.Presentation.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Handlers.Interfaces
{
    public interface IProductHandler
    {
        ResultViewModel<ProductViewModel> SelectAllByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<ProductViewModel> SelectAllNewByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<ProductViewModel> SelectAllRecommendedByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<ProductViewModel> SelectAllSelectedByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<ProductViewModel> SelectAllSoonByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<LableViewModel> SelectAllLableByProductID(bool? activate, int productID, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<ProductViewModel> SelectAllByCategoryType(bool? activate, int categoryTypeID, int? pageNumber = null, int pageSize = 20);

    }
}
