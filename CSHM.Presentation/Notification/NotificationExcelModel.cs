namespace CSHM.Presentation.Notification;

public class NotificationExcelModel
{
    public int ID { get; set; }

    public int NotificationTypeID { get; set; }

    public string NotificationTypeTitle { get; set; }

    public string Subject { get; set; }

    public string MessageContent { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public bool IsActive { get; set; }

}
