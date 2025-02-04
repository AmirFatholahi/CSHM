using CSHM.Domain.Interfaces;


namespace CSHM.Domain;

public class NotificationType : IEntity
{
    public NotificationType() { }

    public int ID { get; set; }

    public string Title { get; set; }//اعلانات سیستمی- اخبار و اطلاعیه ها - پیام های کاربری - پیام کوتاه - ایمیل

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }
}
