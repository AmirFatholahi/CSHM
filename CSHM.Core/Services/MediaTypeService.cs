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
