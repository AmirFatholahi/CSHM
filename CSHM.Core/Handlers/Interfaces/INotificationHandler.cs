using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Domain;
using CSHM.Presentation.OTP;

namespace CSHM.Core.Handlers.Interfaces;

public interface INotificationHandler
{
    void SendNotification(int draftID, List<KeyValueViewModel> list, int userID, int creatorID);

    void SendNotification(int draftID, List<KeyValueViewModel> list, List<int> userIDs, int creatorID);

    void SendNotification(Notification notification, int userID, int creatorID);

    void SendNotification(Notification notification, List<int> userIDs, int creatorID);


    void SMS(int draftID, string cellphone, List<KeyValueViewModel> list, int userID, int creatorID);

    MessageViewModel SendSms(string content);

    void Email(int draftID, string to, List<KeyValueViewModel> list, Dictionary<string, string> attachments, int userID, int creatorID);

    MessageViewModel SendOTP(OTPViewModel entity);


    ResultViewModel<NotificationViewModel> SelectNotificationsByUser(bool? activate, int userID, string filter = null, int? pageNumber = null, int pageSize = 20);

    List<NotificationViewModel> SelectTopNotificationsByUser(int userID);

    NotificationViewModel Select(int notificationOwnerID);

}