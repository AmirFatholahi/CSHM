using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.Calender;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using CSHM.Widget.Calendar;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class CalenderDimensionService : Repository<CalenderDimension, CalenderDimensionViewModel>, ICalenderDimensionService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;

    public CalenderDimensionService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
    }

    /// <summary>
    /// اولین روز  ماه بر حسب تاریخ ورودی
    /// </summary>
    /// <param name="gregDate"></param>
    /// <returns></returns>
    public CalenderDimensionViewModel FirstDateOfMonth(DateTime gregDate)
    {
        CalenderDimensionViewModel result;
        var jalaliDate = CalenderWidget.ToJalaliDate(gregDate);
        result = FirstDateOfMonth(jalaliDate);
        return result;
    }


    /// <summary>
    /// اولین روز  ماه بر حسب تاریخ ورودی
    /// </summary>
    /// <param name="jalaliDate"></param>
    /// <returns></returns>
    public CalenderDimensionViewModel FirstDateOfMonth(string jalaliDate)
    {
        CalenderDimensionViewModel result;
        if (string.IsNullOrWhiteSpace(jalaliDate))
        {
            result = new CalenderDimensionViewModel();
            return result;
        }

        var id = Convert.ToInt32(jalaliDate.Replace("/", ""));
        var currentDate = GetByID(id);
        if (currentDate != null)
        {
            var item = GetAll(true, x =>x.YearKey==currentDate.YearKey && x.MonthKey == currentDate.MonthKey && x.IsFirstOfMonth == true).FirstOrDefault();
            if (item != null)
            {
                result = MapToViewModel(item);
                return result;
            }
            else
            {
                result = new CalenderDimensionViewModel();
                return result;
            }
        }
        else
        {
            result = new CalenderDimensionViewModel();
            return result;
        }
    }


    /// <summary>
    /// آخرین روز ماه بر حسب تاریخ ورودی
    /// </summary>
    /// <param name="gregDate"></param>
    /// <returns></returns>
    public CalenderDimensionViewModel LastDateOfMonth(DateTime gregDate)
    {
        CalenderDimensionViewModel result;
        var jalaliDate = CalenderWidget.ToJalaliDate(gregDate);
        result = LastDateOfMonth(jalaliDate);
        return result;
    }

    /// <summary>
    /// آخرین روز ماه بر حسب تاریخ ورودی
    /// </summary>
    /// <param name="jalaliDate"></param>
    /// <returns></returns>
    public CalenderDimensionViewModel LastDateOfMonth(string jalaliDate)
    {
        CalenderDimensionViewModel result;
        var id = Convert.ToInt32(jalaliDate.Replace("/", ""));
        var currentDate = GetByID(id);
        if (currentDate != null)
        {
            var item = GetAll(true, x => x.YearKey == currentDate.YearKey && x.MonthKey == currentDate.MonthKey && x.IsLastOfMonth == true).FirstOrDefault();
            if (item != null)
            {
                result = MapToViewModel(item);
                return result;
            }
            else
            {
                result = new CalenderDimensionViewModel();
                return result;
            }
        }
        else
        {
            result = new CalenderDimensionViewModel();
            return result;
        }
    }




    public override ResultViewModel<CalenderDimensionViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<CalenderDimensionViewModel>();
        try
        {
            IQueryable<CalenderDimension> items;
            Expression<Func<CalenderDimension, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.GregDate.Contains(filter) || x.HijriDate.Contains(filter) || x.JalaliDate.Contains(filter);
            items = GetAll(activate, condition, pageNumber, pageSize);
            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }


    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(CalenderDimension entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.Date == DateTime.MinValue)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "تاریخ")
            });
        }
        if (string.IsNullOrEmpty(entity.GregDate) || string.IsNullOrWhiteSpace(entity.GregDate))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "تاریخ میلادی")
            });
        }
        if (entity.GregKey == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کلید تاریخ میلادی")
            });
        }
        if (string.IsNullOrEmpty(entity.JalaliDate) || string.IsNullOrWhiteSpace(entity.JalaliDate))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "تاریخ شمسی")
            });
        }
        if (string.IsNullOrEmpty(entity.HijriDate) || string.IsNullOrWhiteSpace(entity.HijriDate))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "تاریخ هجری")
            });
        }
        if (entity.HijriKey == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کلید تاریخ هجری")
            });
        }
        if (entity.YearKey == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کلید سال")
            });
        }
        if (string.IsNullOrEmpty(entity.YearName) || string.IsNullOrWhiteSpace(entity.YearName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "سال")
            });
        }

        //Max Length
        if (!string.IsNullOrEmpty(entity.GregDate) && entity.GregDate.Length > 20)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ میلادی", 20)
            });
        }
        if (!string.IsNullOrEmpty(entity.JalaliDate) && entity.JalaliDate.Length > 20)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ شمسی", 20)
            });
        }
        if (!string.IsNullOrEmpty(entity.HijriDate) && entity.HijriDate.Length > 20)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ هجری", 20)
            });
        }
        if (!string.IsNullOrEmpty(entity.YearName) && entity.YearName.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "سال", 50)
            });
        }

        return result;
    }

}
