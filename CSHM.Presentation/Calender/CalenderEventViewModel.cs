namespace CSHM.Presentations.Calender;

public class CalenderEventViewModel
{
    public int ID { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public string CalenderType { get; set; } // Hijri-Greg-Jalali // H-G-J

    public string Title { get; set; }

    public bool IsHoliday { get; set; }

    public bool IsActive { get; set; }

}