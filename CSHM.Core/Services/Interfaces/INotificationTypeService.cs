using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface INotificationTypeService : IRepository<NotificationType, NotificationTypeViewModel>
{
    HttpResponseMessage ExcelAll();

}
