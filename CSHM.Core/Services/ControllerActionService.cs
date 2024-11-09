using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.Login;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class ControllerActionService : Repository<ControllerAction, ControllerActionViewModel>, IControllerActionService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public ControllerActionService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }

    public override ResultViewModel<ControllerActionViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<ControllerActionViewModel>();
        try
        {
            IQueryable<ControllerAction> items;
            Expression<Func<ControllerAction, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.TitleFa.Contains(filter) || x.TitleEn.Contains(filter) ||
                x.ControllerName.Contains(filter) || x.ActionName.Contains(filter);

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
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }

    public HttpResponseMessage ExcelAll()
    {
        HttpResponseMessage result;
        var items = SelectAll(null).List;
        var list = _mapper.Map<List<ControllerActionExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }

    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(ControllerAction entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.Code == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کد")
            });
        }
        if (string.IsNullOrEmpty(entity.TitleFa) || string.IsNullOrWhiteSpace(entity.TitleFa))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "عنوان فارسی")
            });
        }
        if (string.IsNullOrEmpty(entity.TitleEn) || string.IsNullOrWhiteSpace(entity.TitleEn))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "عنوان انگلیسی")
            });
        }
        if (string.IsNullOrEmpty(entity.ControllerName) || string.IsNullOrWhiteSpace(entity.ControllerName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کنترلر")
            });
        }
        if (string.IsNullOrEmpty(entity.ActionName) || string.IsNullOrWhiteSpace(entity.ActionName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "اکشن")
            });
        }
        if (entity.PageID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "صفحه")
            });
        }

        //Max Length
        if (!string.IsNullOrEmpty(entity.TitleFa) && entity.TitleFa.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان فارسی", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.TitleEn) && entity.TitleEn.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان انگلیسی", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.ControllerName) && entity.ControllerName.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "کنترلر", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.ActionName) && entity.ActionName.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "اکشن", 50)
            });
        }

        return result;
    }

}