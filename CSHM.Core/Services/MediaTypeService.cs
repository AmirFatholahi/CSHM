using AutoMapper;
using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.Media;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class MediaTypeService : Repository<MediaType, MediaTypeViewModel>, IMediaTypeService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public MediaTypeService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }

    public override ResultViewModel<MediaTypeViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<MediaTypeViewModel>();
        try
        {
            IQueryable<MediaType> items;
            Expression<Func<MediaType, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter);
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

  

    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(MediaType entity)
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


        //Max Length
        if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 100)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 100)
            });
        }

        return result;
    }

}
