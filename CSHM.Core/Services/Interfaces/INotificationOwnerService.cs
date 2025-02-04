using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface INotificationOwnerService : IRepository<NotificationOwner, NotificationOwnerViewModel>
{

    KeyValueViewModel Seen(int notificationOwnerID, int modifierID);

   // HttpResponseMessage ExcelAll();

}
