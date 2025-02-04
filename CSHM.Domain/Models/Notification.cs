using CSHM.Domain.Interfaces;


namespace CSHM.Domain;

public class Notification : IEntity
{
    public Notification() { }

    public int ID { get; set; }

    public int NotificationTypeID { get; set; }

    public string Subject { get; set; }

    public string MessageContent { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public string Attachments { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual NotificationType NotificationType { get; set; }

    public virtual User Creator { get; set; }

    public virtual ICollection<NotificationOwner> NotificationOwners { get; set; }
}
