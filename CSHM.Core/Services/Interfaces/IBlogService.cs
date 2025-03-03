using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;

namespace CSHM.Core.Services.Interfaces
{
    public interface IBlogService : IRepository<Blog,BlogViewModel>
    {
        public ResultViewModel<BlogViewModel> SelectAllPinByBlogTypeIDAndPublisherID(int publisherID, int blogTypeID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<BlogViewModel> SelectAllByBlogTypeIDAndPublisherID(int publisherID, int blogTypeID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<BlogViewModel> SelectAllByPublisherID(int publisherID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20);
    }
}
