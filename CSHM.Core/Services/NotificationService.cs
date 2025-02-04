using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class NotificationService : Repository<Notification, NotificationViewModel>, INotificationService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public NotificationService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }

    public  ResultViewModel<NotificationViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<NotificationViewModel>();
        try
        {
            IQueryable<Notification> items;
            Expression<Func<Notification, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Subject.Contains(filter);
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

    public HttpResponseMessage ExcelAll()
    {
        HttpResponseMessage result;
        var items = SelectAll(null).List;
        var list = _mapper.Map<List<NotificationExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }


    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(Notification entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (string.IsNullOrEmpty(entity.Subject) || string.IsNullOrWhiteSpace(entity.Subject))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "موضوع")
            });
        }
        if (string.IsNullOrEmpty(entity.MessageContent) || string.IsNullOrWhiteSpace(entity.MessageContent))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "متن")
            });
        }
        if (string.IsNullOrEmpty(entity.Status) || string.IsNullOrWhiteSpace(entity.Status))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "وضعیت")
            });
        }
        if (entity.NotificationTypeID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نوع")
            });
        }

        //Max Length
        if (!string.IsNullOrEmpty(entity.Subject) && entity.Subject.Length > 250)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "موضوع", 250)
            });
        }
        if (!string.IsNullOrEmpty(entity.Status) && entity.Status.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "وضعیت", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.Path) && entity.Path.Length > 500)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "مسیر", 500)
            });
        }

        return result;
    }

}