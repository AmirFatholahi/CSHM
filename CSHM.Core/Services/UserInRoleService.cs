using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Calendar;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using Xceed.Document.NET;
using System.Linq.Expressions;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class UserInRoleService : Repository<UserInRole, UserInRoleViewModel>, IUserInRoleService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;

    public UserInRoleService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
    }


    /// <summary>
    /// اضافه ویرایش یک نقش
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel AddOrUpdate(UserInRoleViewModel entity, int creatorID)
    {
        MessageViewModel result;
        var exist = GetAll(null, x => x.RoleId == entity.RoleID && x.UserId == entity.UserID).FirstOrDefault();
        if (exist == null)
        {
            result = Add(new UserInRole()
            {
                RoleId = entity.RoleID,
                UserId = entity.UserID,
                ExpiryDate = entity.ExpirationDate == "" ? null : CalenderWidget.ToGregDateTime(entity.ExpirationDate),
                IsActive = true
            }, creatorID);
            return result;
        }
        else
        {
            exist.IsActive = !exist.IsActive;
            if (!string.IsNullOrEmpty(entity.ExpirationDate))
            {
                exist.ExpiryDate = CalenderWidget.ToGregDateTime(entity.ExpirationDate);
            }
            result = Edit(exist, creatorID);
            return result;
        }
    }


    /// <summary>
    /// نغییر وضعیت فعال بودن نقش های یک کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel Activate(int id, int creatorID)
    {
        MessageViewModel result;
        var exist = GetAll(null, x => x.ID==id).FirstOrDefault();
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
            exist.ExpiryDate = null;
            result = Edit(exist, creatorID);
            return result;
        }
    }


    /// <summary>
    /// دسترسی به نقش کاربر پایه عمومی
    /// </summary>
    /// <param name="userID">شناسه کاربر</param>
    /// <param name="creatorID">سازنده</param>
    /// <returns></returns>
    public MessageViewModel AddRoleBase(int userID, int creatorID)
    {
        MessageViewModel result;
        var exist = GetAll(null, x => x.RoleId == 6 && x.UserId == userID).FirstOrDefault();
        if (exist == null)
        {
            //کاربر پایه عمومی
            result = Add(new UserInRole()
            {
                UserId = userID,
                RoleId = 6,
                ExpiryDate = null,
                IsActive = true
            }, creatorID);
            return result;
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.UserInRoleExisted,
                Errors = null,
                ID = 0,
                Value = ""
            };
            return result;
        }

    }


    /// <summary>
    /// دسترسی به نقش خاص
    /// </summary>
    /// <param name="userID">شناسه کاربر</param>
    /// <param name="creatorID">سازنده</param>
    /// <returns></returns>
    public MessageViewModel AddRole(int userID, int roleID, int creatorID)
    {
        MessageViewModel result;
        var exist = GetAll(null, x => x.RoleId == roleID && x.UserId == userID).FirstOrDefault();
        if (exist == null)
        {
            result = Add(new UserInRole()
            {
                UserId = userID,
                RoleId = roleID,
                ExpiryDate = null,
                IsActive = true
            }, creatorID);
            return result;
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.UserInRoleExisted,
                Errors = null,
                ID = 0,
                Value = ""
            };
            return result;
        }
    }


    //public override ResultViewModel<UserInRoleViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<UserInRoleViewModel>();
    //    try
    //    {
    //        var items = GetAll(activate, null, pageNumber, pageSize);
    //        result.List = MapToViewModel(items);

    //        result.TotalCount = Count(activate);

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


    /// <summary>
    /// دریافت نقش های یک کاربر
    /// </summary>
    /// <param name="activate"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public ResultViewModel<UserInRoleViewModel> SelectRolesByUser(bool? activate, int userID)
    {
        var result = new ResultViewModel<UserInRoleViewModel>();
        try
        {
            Expression<Func<UserInRole, bool>> condition= x => x.UserId == userID;

            var items = GetAll(activate, condition, 1, 100, o => o.ID,true).ToList();
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


    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private List<ErrorViewModel> ValidateForm(UserInRole entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (entity.RoleId <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نقش")
            });
        }
        if (entity.UserId <= 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "کاربر")
            });
        }


        return result;
    }

    public override ResultViewModel<UserInRoleViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        throw new NotImplementedException();
    }
}
