using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Blog;

namespace CSHM.Core.Services.Interfaces;

public interface IBlogTypeService : IRepository<BlogType, BlogTypeViewModel>
{
}
