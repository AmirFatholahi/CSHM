using AutoMapper;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.Media;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.File;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;


namespace CSHM.Core.Services;

public class ExtensionTypeService : Repository<ExtensionType, ExtensionTypeViewModel>, IExtensionTypeService
{
    private readonly ILogWidget _log;

    public ExtensionTypeService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
    {
        _log = log;
    }

    public override MessageViewModel Add(ExtensionType entity, int userID)
    {
        try
        {
            var exist = GetAll(null, x => x.Title == entity.Title).FirstOrDefault();
            if (exist == null)
            {
                return base.Add(entity, userID);
            }
            else
            {
                return new MessageViewModel()
                {
                    ID = -1,
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.RecordExisted,
                    Value = ""
                };
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), userID);
            var errors = new List<ErrorViewModel>();
            errors.Add(new ErrorViewModel() { ErrorCode = ex.HResult.ToString(), ErrorMessage = _log.GetExceptionMessage(ex) });
            return new MessageViewModel()
            {
                ID = -1,
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.SaveFailed,
                Value = "",
                Errors = errors
            };
        }
    }

    //public override ResultViewModel<ExtensionTypeViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<ExtensionTypeViewModel>();
    //    try
    //    {
    //        IQueryable<ExtensionType> items;
    //        if (!string.IsNullOrWhiteSpace(filter))
    //        {
    //            items = GetAll(activate, x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter), pageNumber, pageSize);
    //        }
    //        else
    //        {
    //            items = GetAll(activate, null, pageNumber, pageSize);
    //        }

    //        result.List = MapToViewModel(items);

    //        result.TotalCount = Count(activate, x => string.IsNullOrWhiteSpace(filter) || x.Title.Contains(filter));

    //        result.Message = result.TotalCount > 0
    //            ? new MessageViewModel { Status = Statuses.Success }
    //            : new MessageViewModel
    //            { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
    //        var errors = new List<ErrorViewModel>();
    //        errors.Add(new ErrorViewModel() { ErrorCode = ex.HResult.ToString(), ErrorMessage = _log.GetExceptionMessage(ex) });
    //        result.Message = new MessageViewModel { Status = Statuses.Error, Message = Messages.UnknownException, Errors = errors };
    //        return result;
    //    }
    //}

}
