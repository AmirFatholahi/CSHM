namespace CSHM.Presentation.Notification;

public class NotificationDraftExcelModel
{
    public int ID { get; set; }

    public int PriorityTypeID { get; set; }

    public string PriorityTypeTitle { get; set; }

    public string Title { get; set; }

    public string Subject { get; set; }

    public string MessageBody { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public bool IsActive { get; set; }

}
