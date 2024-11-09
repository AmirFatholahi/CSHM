using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentations.Calender;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Presentations.Calender;
using CSHM.Widget.Calendar;
using CSHM.Widget.Convertor;
using CSHM.Widget.Dapper;
using CSHM.Widget.Log;

namespace CSHM.Core.Handlers;

public class CalenderHandler : ICalenderHandler
{
    private readonly ILogWidget _log;
    private readonly IDapperWidget _dapper;
    private readonly ICalenderDimensionService _calenderDimensionService;

    public CalenderHandler(ILogWidget log, IDapperWidget dapper,ICalenderDimensionService calenderDimensionService)
    {
        _log = log;
        _dapper = dapper;
        _calenderDimensionService = calenderDimensionService;

    }

    public CalenderDimensionViewModel AddDaysToJalaliDate(string jalaliDate, int count, bool onlyWokdays)
    {
        CalenderDimensionViewModel result;
        var first = SelectDate(jalaliDate, CalenderType.Jalali);
        if (first != null)
        {
            if (onlyWokdays == true)
            {
                var next = _calenderDimensionService.GetAll(true, x => x.WorkdayIndex == first.WorkdayIndex + count).FirstOrDefault();
                result = _calenderDimensionService.MapToViewModel(next);
                return result;
            }
            else
            {
                var next = _calenderDimensionService.GetAll(true, x => x.DayIndex == first.DayIndex + count).FirstOrDefault();
                result = _calenderDimensionService.MapToViewModel(next);
                return result;
            }

        }
        else
            return null;

    }

    public CalenderDimensionViewModel AddMonthesToJalaliDate(string jalaliDate, int count)
    {
        CalenderDimensionViewModel result;
        var first = SelectDate(jalaliDate, CalenderType.Jalali);
        if (first != null)
        {
            var dayKey = jalaliDate.Substring(8, 2);
            var next = _calenderDimensionService.GetAll(true, x => x.MonthIndex == first.MonthIndex + count && x.DayKey==dayKey.ToInt()).FirstOrDefault();
            result = _calenderDimensionService.MapToViewModel(next);
            return result;

        }
        else
            return null;
    }

    public CalenderDimensionViewModel AddYearsToJalaliDate(string jalaliDate, int count)
    {
        CalenderDimensionViewModel result;
        var first = SelectDate(jalaliDate, CalenderType.Jalali);
        if (first != null)
        {
            var next = _calenderDimensionService.GetAll(true, x => x.YearKey == first.YearKey + count).FirstOrDefault();
            result = _calenderDimensionService.MapToViewModel(next);
            return result;

        }
        else
            return null;
    }

    /// <summary>
    /// بررسی تعطیل بودن یک روز شمسی
    /// </summary>
    /// <param name="jalaliDate"> تاریخ شمسی</param>
    /// <returns></returns>
    public bool IsHoliday(string jalaliDate)
    {

        var record = SelectDate(jalaliDate, CalenderType.Jalali);
        if (record != null)
        {
            if (record.IsHoliday == true)
                return true;
            else
                return false;
        }
        else
            return false;
    }


    /// <summary>
    /// بررسی کبیسه بودن یک سال شمسی
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool IsLeapYear(string jalaliDate)
    {
        var record = SelectDate(jalaliDate, CalenderType.Jalali);
        if (record != null)
        {
            if (record.IsLeap == true)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public CalenderDimensionViewModel Now()
    {
        var result = SelectDate(DateTime.Now.ToShortDateString(), CalenderType.Greg);
        if (result != null)
            return result;
        else
            return null;
    }

    /// <summary>
    /// تفاوت دو روز شمسی
    /// </summary>
    /// <param name="olderJalaliDate">تاریخ قدیمی تر</param>
    /// <param name="newerJalaliDate">تاریخ جدیدتر</param>
    /// <param name="onlyWorkdays">آیا فقط روزهای کاری؟</param>
    /// <returns></returns>
    public int QuantityOfDays(string olderJalaliDate, string newerJalaliDate, bool onlyWorkdays)
    {
        int result = 0;
        int reverseKey = 1;
        int olderKey = CalenderWidget.DateToKey(olderJalaliDate);
        int newerKey = CalenderWidget.DateToKey(newerJalaliDate);
        if (olderKey > newerKey)
        {
            int temp = olderKey;
            olderKey = newerKey;
            newerKey = temp;
            reverseKey = -1;
        }
        if (onlyWorkdays == true)
        {
            result = _calenderDimensionService.GetAll(true, x => x.ID >= olderKey && x.ID <= newerKey && x.IsWorkDay == true).Count();
        }
        else
        {
            result = _calenderDimensionService.GetAll(true, x => x.ID >= olderKey && x.ID <= newerKey).Count();
        }
        return result * reverseKey;

    }

    /// <summary>
    /// انتخاب یک روز خاص
    /// </summary>
    /// <param name="dateKey">کلید تاریخ</param>
    /// <param name="type">نوع تقویم</param>
    /// <returns></returns>
    public CalenderDimensionViewModel SelectDate(string date, CalenderType type)
    {
        CalenderDimensionViewModel result = new CalenderDimensionViewModel();
        // CalenderDimension record;
        //int dateKey = CalenderWidget.DateToKey(date);
        //if (type == CalenderType.Jalali)
        //{
        //    record = _calenderDimensionService.GetAll(true, x => x.ID == dateKey).FirstOrDefault();
        //    result = _calenderDimensionService.MapToViewModel(record);
        //}
        //else if (type == CalenderType.Greg)
        //{
        //    record = _calenderDimensionService.GetAll(true, x => x.GregKey == dateKey).FirstOrDefault();
        //    result = _calenderDimensionService.MapToViewModel(record);
        //}
        //else if (type == CalenderType.Hijri)
        //{
        //    record = _calenderDimensionService.GetAll(true, x => x.HijriKey == dateKey).FirstOrDefault();
        //    result = _calenderDimensionService.MapToViewModel(record);
        //}
        //else
        //    result = null;
        //var now = DateTime.Now;
        //result.Time = now.ToString("HH:mm");
        //result.JalaliDateTime = result.JalaliDate + " | " + result.Time;
        return result;
    }
}