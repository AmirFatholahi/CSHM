using System.Linq.Expressions;
using System.Reflection;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using CSHM.Presentation.Resources;
using CSHM.Presentation.Base;
using CSHM.Presentations.User;

namespace CSHM.Core.Services;

public class PolicyParameterService : Repository<PolicyParameter, PolicyParameterViewModel>, IPolicyParameterService
{

    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public PolicyParameterService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }

    public override ResultViewModel<PolicyParameterViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// دریافت پارامترها با استفاده از ساید
    /// </summary>
    /// <param name="activate"></param>
    /// <param name="side">ساید</param>
    /// <returns></returns>
    public  List<PolicyParameterViewModel> SelectAllBySide(bool? activate, string side)
    {
        var result = new List<PolicyParameterViewModel>();
        try
        {
            Expression<Func<PolicyParameter, bool>> condition = x => x.Side == side;
            var items = GetAll(activate, condition);
            result = MapToViewModel(items);
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            return result;
        }
    }


    //public override ResultViewModel<PolicyParameterViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<PolicyParameterViewModel>();
    //    try
    //    {
    //        IQueryable<PolicyParameter> items;
    //        Expression<Func<PolicyParameter, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter);
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
    //        var errors = new List<ErrorViewModel>();
    //        errors.Add(new ErrorViewModel() { ErrorCode = ex.HResult.ToString(), ErrorMessage = _log.GetExceptionMessage(ex) });
    //        result.Message = new MessageViewModel { Status = Statuses.Error, Message = Messages.UnknownException, Errors = errors };
    //        return result;
    //    }
    //}
}