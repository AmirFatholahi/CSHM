using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.User;
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

public class PolicyService : Repository<Policy, PolicyViewModel>, IPolicyService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public PolicyService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }


    /// <summary>
    /// دریافت پالیسی ها با استفاده از شناسه نوع
    /// </summary>
    /// <param name="activate"></param>
    /// <param name="policyParameterID">شناسه نوع پالیسی</param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public ResultViewModel<PolicyViewModel> SelectAll(bool? activate, int policyParameterID, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<PolicyViewModel>();
        try
        {
            Expression<Func<Policy, bool>> condition = x => x.PolicyParameterID == policyParameterID && (string.IsNullOrWhiteSpace(filter) || x.Key.Contains(filter) || x.Value.Contains(filter));
            var items = GetAll(activate, condition, pageNumber, pageSize);
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
            var errors = new List<ErrorViewModel>();
            errors.Add(new ErrorViewModel() { ErrorCode = ex.HResult.ToString(), ErrorMessage = _log.GetExceptionMessage(ex) });
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = Messages.UnknownException, Errors = errors };
            return result;
        }
    }



    public override ResultViewModel<PolicyViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<PolicyViewModel>();
        try
        {
            IQueryable<Policy> items;
            Expression<Func<Policy, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Key.Contains(filter) || x.Value.Contains(filter);
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
            var errors = new List<ErrorViewModel>();
            errors.Add(new ErrorViewModel() { ErrorCode = ex.HResult.ToString(), ErrorMessage = _log.GetExceptionMessage(ex) });
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = Messages.UnknownException, Errors = errors };
            return result;
        }
    }



}