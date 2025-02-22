using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;

namespace CSHM.Core.Services.Interfaces
{
    public interface IBlogService : IRepository<Blog,BlogViewModel>
    {
        public ResultViewModel<BlogViewModel> SelectAllByPublisherID(int publisherID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);
    }
}
