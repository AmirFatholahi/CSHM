using AutoMapper;
using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.General;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class SettingService : Repository<Setting, SettingViewModel>, ISettingService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;

    public SettingService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
    }

    //public override ResultViewModel<SettingViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<SettingViewModel>();
    //    try
    //    {
    //        IQueryable<Setting> items;
    //        Expression<Func<Setting, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter);
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

    public List<SettingViewModel> SelectAllByGroup(string groupCode)
    {
        List<SettingViewModel> result = new List<SettingViewModel>();
        var items = GetAll(true, x => x.GroupCode == groupCode).ToList();
        result = MapToViewModel(items);
        return result;
    }

}