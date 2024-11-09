using System.Reflection;

using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using System.Linq.Expressions;
using CSHM.Presentation.Base;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class UserPolicyService : Repository<UserPolicy, UserPolicyViewModel>, IUserPolicyService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public UserPolicyService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }



    /// <summary>
    /// درج و ویرایش یک سیاست کاربر
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel AddOrUpdate(UserPolicyViewModel entity, int creatorID)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        var exist = GetAll(null, x => x.UserID == entity.UserID && x.PolicyID == entity.PolicyID).FirstOrDefault();
        if (exist== null)
        {
            result = Add(new UserPolicy()
            {
                PolicyID=entity.PolicyID,
                UserID=entity.UserID,
                IsActive=true
            }, creatorID);
            return result;
        }
        else
        {
            errors.Add(new ErrorViewModel()
            {
                ErrorCode="",
                ErrorMessage=Messages.RecordExisted
            });

            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.SaveFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }
    }


    /// <summary>
    /// نغییر وضعیت فعال بودن سیسات های یک کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel Activate(int id, int creatorID)
    {
        MessageViewModel result;
        var exist = GetAll(null, x => x.ID == id).FirstOrDefault();
        if (exist == null)
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.RecordNotFound,
                Errors = null,
                ID = 0,
                Value = ""
            };
            return result;
        }
        else
        {
            exist.IsActive = !exist.IsActive;
            result = Edit(exist, creatorID);
            return result;
        }
    }




    /// <summary>
    /// لیست تمام پالیسی های یک کاربر
    /// </summary>
    /// <param name="activate">وضعیت</param>
    /// <param name="userID">شناسه کاربر</param>
    /// <returns></returns>
    public ResultViewModel<UserPolicyViewModel> SelectAllUserPolicies(bool? activate, int userID)
    {
        var result = new ResultViewModel<UserPolicyViewModel>();
        var items = GetAll(activate, x => x.UserID == userID);
        result.List = MapToViewModel(items);

        result.TotalCount = Count(activate, x => x.UserID == userID);

        result.Message = result.TotalCount > 0
            ? new MessageViewModel { Status = Statuses.Success }
            : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
        return result;

    }

    /// <summary>
    /// لیست پالیسی های یک کاربر با توجه به نوع پارامتر
    /// </summary>
    /// <param name="activate"> وضعیت</param>
    /// <param name="userID">شناسه کاربر</param>
    /// <param name="policyParameterID">نوع پارامتر</param>
    /// <returns></returns>
    public ResultViewModel<UserPolicyViewModel> SelectAllUserPolicies(bool? activate, int userID, int policyParameterID)
    {
        var result = new ResultViewModel<UserPolicyViewModel>();
        var items = GetAll(activate, x => x.UserID == userID && x.Policy.PolicyParameterID == policyParameterID);
        result.List = MapToViewModel(items);

        result.TotalCount = Count(activate, x => x.UserID == userID && x.Policy.PolicyParameterID == policyParameterID);

        result.Message = result.TotalCount > 0
            ? new MessageViewModel { Status = Statuses.Success }
            : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
        return result;
    }

    /// <summary>
    /// لیست سیاست های یک کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="activate"></param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public ResultViewModel<UserPolicyViewModel> SelectAllByUser(int userID, bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<UserPolicyViewModel>();
        try
        {
            IQueryable<UserPolicy> items;
            Expression<Func<UserPolicy, bool>> condition = x => x.UserID == userID && (string.IsNullOrWhiteSpace(filter) || x.Policy.Key.Contains(filter) || x.Policy.Value.Contains(filter) || x.Policy.PolicyParameter.Title.Contains(filter));
            items = GetAll(activate, condition, pageNumber, pageSize, o => o.ID, true);
            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate);

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

    public override ResultViewModel<UserPolicyViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<UserPolicyViewModel>();
        try
        {
            var items = GetAll(activate, null, pageNumber, pageSize);
            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate);

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



    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(UserPolicy entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.UserID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کاربر")
            });
        }
        if (entity.PolicyID <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "روش")
            });
        }

        return result;
    }

}