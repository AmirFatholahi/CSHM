using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.Role;
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

public class RoleClaimService : Repository<RoleClaim, RoleClaimViewModel>, IRoleClaimService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public RoleClaimService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }


    private List<ErrorViewModel> ValidateForm(RoleClaim entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.ControllerActionCode <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کد اکشن")
            });
        }
        if (entity.RoleID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نقش")
            });
        }

        return result;
    }

    //public override ResultViewModel<RoleClaimViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<RoleClaimViewModel>();
    //    try
    //    {
    //        var items = GetAll(activate, x => string.IsNullOrWhiteSpace(filter), pageNumber, pageSize);
    //        result.List = MapToViewModel(items);

    //        result.TotalCount = Count(activate, x => string.IsNullOrWhiteSpace(filter));

    //        result.Message = result.TotalCount > 0
    //            ? new MessageViewModel { Status = Statuses.Success }
    //            : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
    //        result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
    //        return result;
    //    }
    //}

    public HttpResponseMessage ExcelAll()
    {
        HttpResponseMessage result;
        var items = GetAll(null).ToList();
        var list = _mapper.Map<List<RoleClaimExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }

    public override ResultViewModel<RoleClaimViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        throw new NotImplementedException();
    }
}
