using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class CalenderEvent:IEntity
{
    public CalenderEvent() { }

    public int ID { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public string CalenderType { get; set; } // Hijri-Greg-Jalali // H-G-J

    public string Title { get; set; }

    public bool IsHoliday { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }
}