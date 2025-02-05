using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Calendar;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class NotificationOwnerService : Repository<NotificationOwner, NotificationOwnerViewModel>, INotificationOwnerService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public NotificationOwnerService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }


   /// <summary>
   /// تغییر وضعیت پیام به حالت مشاهده شده
   /// </summary>
   /// <param name="notificationOwnerID">شناسه </param>
   /// <param name="modifierID">کاربر مشاهده کننده</param>
    public KeyValueViewModel Seen(int notificationOwnerID,int modifierID)
    {
        KeyValueViewModel result ;
        var item = GetByID(notificationOwnerID);
        if (item != null)
        {
            if (item.IsActive == true)
            {
                item.IsActive = false;
                item.SeenDateTime = CalenderWidget.ToJalaliDateTime(DateTime.Now);
                Edit(item, modifierID);
                result = new KeyValueViewModel()
                {
                    Key = "SeenDateTime",
                    Value = item.SeenDateTime
                };
                return result;
            }
            else
            {
                result = new KeyValueViewModel()
                {
                    Key = "SeenDateTime",
                    Value = item.SeenDateTime
                };
                return result;
            }
        }
        else
        {
            return null;
        }
    }
    

    public override ResultViewModel<NotificationOwnerViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<NotificationOwnerViewModel>();
        try
        {
            IQueryable<NotificationOwner> items;
            Expression<Func<NotificationOwner, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.SeenDateTime.Contains(filter);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                items = GetAll(activate, condition, pageNumber, pageSize);
            }
            else
            {
                items = GetAll(activate, null, pageNumber, pageSize);
            }
            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }

    //public HttpResponseMessage ExcelAll()
    //{
    //    HttpResponseMessage result;
    //    var items = SelectAll(null).List;
    //    var list = _mapper.Map<List<NotificationOwnerExcelModel>>(items);

    //    result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

    //    return result;
    //}


    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(NotificationOwner entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.NotificationID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "پیام")
            });
        }
        if (entity.UserID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کاربر")
            });
        }

        //Max Length
        if (!string.IsNullOrEmpty(entity.SeenDateTime) && entity.SeenDateTime.Length > 10)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ مشاهده", 10)
            });
        }

        return result;
    }

}
