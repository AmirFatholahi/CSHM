namespace CSHM.Presentation.Notification;

public class NotificationViewModel
{
    public int ID { get; set; }

    public int NotificationTypeID { get; set; }

    public string NotificationTypeTitle { get; set; }

    public int NotificationOwnerID { get; set; }

    public int PrirityTypeID { get; set; }

    public string PriorityTypeTitle { get; set; }

    public string Subject { get; set; }

    public string MessageContent { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public string SeenDateTime { get; set; }

    public string CreationDateTime { get; set; }

    public int CreatorID { get; set; }

    public string CreatorName { get; set; }

    public bool IsActive { get; set; }

}
