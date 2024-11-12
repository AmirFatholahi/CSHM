using CSHM.Presentation.Base;
using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentations.Media;

namespace CSHM.Core.Services.Interfaces;

public interface IMediaService : IRepository<Media, MediaViewModel>
{
    ResultViewModel<MediaViewModel> SelectAllByEntity<T>(bool? activate, int entityID, bool? isDefault = null, bool? showIsConfirmed = null, string? filter = null, int? pageNumber = null, int pageSize = 20);

 //   ResultViewModel<MediaViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20);

   // ResultViewModel<MediaViewModel> SelectAll(string entityName, int entityID, bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20);

    List<ErrorViewModel> ValidateForm(Media entity);

    Guid SetMedia(MediaViewModel entity);

    MediaTempViewModel GetMedia(string fileName);

    FileResultViewModel ImageNotFound();

    MessageViewModel SetAsDefault(int id, int creatorID);

    MessageViewModel SetAllDefaultToFalse(string entityName, int entityID, int currentID, int creatorID);

}