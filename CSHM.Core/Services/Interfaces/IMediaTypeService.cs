
using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentations.Media;

namespace CSHM.Core.Services.Interfaces;

public interface IMediaTypeService : IRepository<MediaType, MediaTypeViewModel>
{
}