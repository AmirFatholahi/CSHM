using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class CalenderDimension : IEntity
{
    public CalenderDimension() { }

    public int ID { get; set; }

    public DateTime Date { get; set; }

    public string GregDate { get; set; }

    public int GregKey { get; set; }

    public string JalaliDate { get; set; }

    public string HijriDate { get; set; }

    public int HijriKey { get; set; }

    public int YearKey { get; set; }

    public string YearName { get; set; }

    public int SeasonKey { get; set; }

    public string SeasonName { get; set; }

    public int MonthKey { get; set; }

    public string MonthName { get; set; }

    public int WeekOfYearKey { get; set; }

    public int DayOfWeekKey { get; set; }

    public string DayOfWeekName { get; set; }

    public int DayKey { get; set; }

    public string DayName { get; set; }

    public int DayOfYearKey { get; set; }

    public string JalaliAlphabetic { get; set; }

    public int GregYearKey { get; set; }

    public int GregSeasonKey { get; set; }

    public int GregMonthKey { get; set; }

    public int GregWeekKey { get; set; }

    public int GregDayKey { get; set; }

    public string GregSeasonName { get; set; }

    public string GregSeasonNameFa { get; set; }

    public string GregMonthName { get; set; }

    public string GregMonthNameFa { get; set; }

    public int SeasonIndex { get; set; }

    public int MonthIndex { get; set; }

    public int WeekIndex { get; set; }

    public int DayIndex { get; set; }

    public int WorkdayIndex { get; set; }

    public int MidYear { get; set; }

    public int MidSeason { get; set; }

    public int MidMonth { get; set; }

    public bool IsHoliday { get; set; }

    public bool IsWorkDay { get; set; }

    public bool IsFirstOfYear { get; set; }

    public bool IsLastOfYear { get; set; }

    public bool IsFirstOfSeason { get; set; }

    public bool IsLastOfSeason { get; set; }

    public bool IsFirstOfMonth { get; set; }

    public bool IsLastOfMonth { get; set; }

    public bool IsFirstOfWeek { get; set; }

    public bool IsLastOfWeek { get; set; }

    public bool IsLeap { get; set; }

    public string Identifier { get; set; }

    public string Hash { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual ICollection<CalenderEvent> CalenderEvents { get; set; }
}