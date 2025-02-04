namespace CSHM.Presentation.Notification;

public class NotificationOwnerExcelModel
{
    public int ID { get; set; }

    public int NotificationID { get; set; }

    public string NotificationSubject { get; set; }

    public int UserID { get; set; }

    public string SeenDateTime { get; set; }

    public bool IsActive { get; set; }

}
