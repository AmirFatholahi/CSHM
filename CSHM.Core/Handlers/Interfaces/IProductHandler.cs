using CSHM.Presentation.Base;
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
    }
}
