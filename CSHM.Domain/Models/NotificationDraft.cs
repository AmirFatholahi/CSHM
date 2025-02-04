using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class NotificationDraft : IEntity
{
    public NotificationDraft() { }

    public int ID { get; set; }

    public int NotificationTypeID { get; set; }

    public string Title { get; set; }

    public string Subject { get; set; }

    public string MessageBody { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }
}
