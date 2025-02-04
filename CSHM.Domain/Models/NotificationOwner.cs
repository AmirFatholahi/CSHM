using CSHM.Domain.Interfaces;


namespace CSHM.Domain;

public class NotificationOwner : IEntity
{
    public NotificationOwner() { }

    public int ID { get; set; }

    public int NotificationID { get; set; }

    public int UserID { get; set; }

    public string SeenDateTime { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }


    public virtual Notification Notification { get; set; }

    public virtual User User { get; set; }
}
