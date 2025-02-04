using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface INotificationDraftService : IRepository<NotificationDraft, NotificationDraftViewModel>
{
    HttpResponseMessage ExcelAll();

    string ContentConstructor(string messageBody, List<KeyValueViewModel> list);

}
