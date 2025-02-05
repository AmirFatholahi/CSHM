using System.Linq.Expressions;
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

public class RoleService : Repository<Role, RoleViewModel>, IRoleService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public RoleService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }


    private List<ErrorViewModel> ValidateForm(Role entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrWhiteSpace(entity.Name))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نام")
            });
        }

        //Max Length
        if (!string.IsNullOrEmpty(entity.Name) && entity.Name.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نام", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.NormalizedName) && entity.NormalizedName.Length > 50)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نام نرمالایز شده", 50)
            });
        }
        if (!string.IsNullOrEmpty(entity.ConcurrencyStamp) && entity.ConcurrencyStamp.Length > 500)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error931,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "برچسب همزمانی", 500)
            });
        }

        return result;
    }

    //public override ResultViewModel<RoleViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<RoleViewModel>();
    //    try
    //    {
    //        IQueryable<Role> items;
    //        Expression<Func<Role, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Name.Contains(filter);
    //        if (!string.IsNullOrWhiteSpace(filter))
    //        {
    //            items = GetAll(activate, condition, pageNumber, pageSize);
    //        }
    //        else
    //        {
    //            items = GetAll(activate, null, pageNumber, pageSize);
    //        }
    //        result.List = MapToViewModel(items);

    //        result.TotalCount = Count(activate, condition);

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
        var list = _mapper.Map<List<RoleExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }

    public override ResultViewModel<RoleViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        throw new NotImplementedException();
    }
}
