using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface INotificationService : IRepository<Notification, NotificationViewModel>
{
    HttpResponseMessage ExcelAll();
}
