using CSHM.Presentations.Calender;

namespace CSHM.Core.Handlers.Interfaces;

public enum CalenderType
{
    Jalali = 1,
    Greg = 2,
    Hijri = 3
}
public interface ICalenderHandler
{

    CalenderDimensionViewModel SelectDate(string date, CalenderType type);

    CalenderDimensionViewModel AddDaysToJalaliDate(string jalaliDate, int count,bool onlyWokdays);

    CalenderDimensionViewModel AddMonthesToJalaliDate(string jalaliDate, int count);

    CalenderDimensionViewModel AddYearsToJalaliDate(string jalaliDate, int count);

    int QuantityOfDays(string olderJalaliDate, string newerJalaliDate, bool onlyWorkdays);


    CalenderDimensionViewModel Now();

    bool IsLeapYear(string jalaliDate);

    bool IsHoliday(string jalaliDate);




}