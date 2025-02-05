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

public class NotificationDraftService : Repository<NotificationDraft, NotificationDraftViewModel>, INotificationDraftService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public NotificationDraftService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }

    /// <summary>
    /// سازنده متن پیش نویس
    /// </summary>
    /// <param name="messageBody">متن پیش نویس</param>
    /// <param name="list">مقادیر اصلی جایگزین شونده</param>
    /// <returns></returns>
    public string ContentConstructor(string messageBody, List<KeyValueViewModel> list)
    {
        string result = string.Empty;
        if (!string.IsNullOrWhiteSpace(messageBody) && list.Any())
        {
            foreach (var item in list)
            {
                messageBody = messageBody.Replace(item.Key, item.Value);
            }
        }
        result = messageBody;
        return result;
    }


    public override  ResultViewModel<NotificationDraftViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<NotificationDraftViewModel>();
        try
        {
            IQueryable<NotificationDraft> items;
            Expression<Func<NotificationDraft, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter);
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
        var list = _mapper.Map<List<NotificationDraftExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }


    /// <summary>
    /// اعتبارسنجی فرم 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(NotificationDraft entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (string.IsNullOrEmpty(entity.Title) || string.IsNullOrWhiteSpace(entity.Title))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "عنوان")
            });
        }
        if (string.IsNullOrEmpty(entity.Subject) || string.IsNullOrWhiteSpace(entity.Subject))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "موضوع")
            });
        }
        if (string.IsNullOrEmpty(entity.MessageBody) || string.IsNullOrWhiteSpace(entity.MessageBody))
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

        //Max Length
        if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 250)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 250)
            });
        }
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
