using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Publish;

namespace CSHM.Core.Services.Interfaces;

public interface IPublisherService : IRepository<Publisher,PublisherViewModel>
{
    public  ResultViewModel<PublisherViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20);
}
