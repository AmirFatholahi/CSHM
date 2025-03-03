using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentations.User;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.OTP;
using CSHM.Widget.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nest;
using CSHM.Widget.Image;
using Microsoft.AspNetCore.Hosting;
using CSHM.Widget.Email;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CSHM.Widget.Calendar;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using CSHM.Presentation.Base;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Services;

public class UserService : Repository<User, UserViewModel>, IUserService
{
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;
    private readonly IEmailWidget _email;
    private readonly UserManager<User> _userManager;
    private readonly DatabaseContext _context;
    private readonly IHostingEnvironment _hostingEnvironment;

    public UserService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IEmailWidget email, UserManager<User> userManager, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
        _userManager = userManager;
        _email = email;
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    /// <summary>
    /// بررسی احراز هویت کاربر بصورت ساده
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public User CheckUser(string username, string password)
    {
        var user = _userManager.FindByNameAsync(username).Result;
        if (user == null)
        {
            return null;
        }
        if (user.LockoutEnabled == true)
        {
            return null;
        }

        if (user.IsActive == false)
        {
            return null;
        }

        var isAuthenticated = _userManager.CheckPasswordAsync(user, password).Result;
        if (isAuthenticated == false)
        {
            return null;
        }
        return user;
    }

    /// <summary>
    /// تغییر وضعیت کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel UserActivate(int id, int creatorID)
    {
        MessageViewModel result;
        try
        {
            var user = GetAll(null, x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                result = new MessageViewModel()
                {
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.UserNotFound,
                    ID = 0,
                    Value = ""
                };
                return result;
            }
            user.IsActive = !user.IsActive;
            result = Edit(user, creatorID);
            return result;

        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            return null;
        }
    }



    /// <summary>
    /// ایجاد کاربر جدید
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="creatorID">کاربر سازنده رکورد</param>
    /// <returns></returns>
    public MessageViewModel GenerateUser(User entity, int creatorID, string ip)
    {
        MessageViewModel result;
        try
        {
            var exist = GetAll(null, x => x.NormalizedUserName == entity.UserName.ToUpper()).FirstOrDefault();
            if (exist == null)
            {
                var password = GenerateRandomPassword();
                entity.NormalizedUserName = entity.UserName.ToUpper();
                entity.EmailConfirmed = false;
             //   entity.Avatar = CreateRandomAvatar(entity.GenderTypeID == 0 ? 1 : entity.GenderTypeID);
                entity.LockoutEnabled = false;
                entity.SecretKey = OTPWidget.GenerateSecretKey(entity.UserName);
                entity.AccessFailedCount = 0;
                entity.IsActive = true;
                entity.CreationDateTime = DateTime.Now;
                entity.CreatorID = creatorID;
                entity.RegistrationDate = CalenderWidget.ToJalaliDate(DateTime.Now, "/");
                var u = _userManager.CreateAsync(entity, password).Result;

                //fetch current user after creation
                var currentUser = _userManager.FindByNameAsync(entity.UserName).Result;
                currentUser.LockoutEnabled = false;
                Edit(currentUser, creatorID);

                //add password log
                _log.UserLog("CSHM", currentUser.ID, "USER_Create", string.Empty, ip);
                var hash = SecurityWidget.GetSHA256(password);
                _log.PasswordLog(currentUser.ID, hash, ip);

                result = new MessageViewModel()
                {
                    Status = Statuses.Success,
                    Title = Titles.Save,
                    Message = Messages.SaveSuccessed,
                    ID = currentUser.ID,
                    Value = password
                };
                return result;
            }
            else
            {
                result = new MessageViewModel()
                {
                    ID = -1,
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.RecordExisted,
                    Value = ""
                };
                return result;
            }
        }
        catch (Exception ex)
        {

            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), creatorID);
            result = new MessageViewModel()
            {
                ID = -1,
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.SaveFailed,
                Value = ""
            };
            return result;
        }
    }


    /// <summary>
    /// سازنده تصادفی آواتار
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private string CreateRandomAvatar(int genderTypeID)
    {
        try
        {
            Random rnd = new Random();
            var number = rnd.Next(1, 4);
            var path = $"{_hostingEnvironment.ContentRootPath}\\wwwroot\\avatars\\avatar-{genderTypeID}-{number}.png";
            if (!File.Exists(path))
            {
                path = $"{_hostingEnvironment.ContentRootPath}\\wwwroot\\avatars\\avatar-1-1.png";
            }

            var byteArray = ImageWidget.ToBinary(path);
            var result = ImageWidget.ToBase64(byteArray);
            return result;
        }
        catch (Exception ex)
        {
            var path = $"{_hostingEnvironment.ContentRootPath}\\wwwroot\\avatars\\avatar-1-1.png";
            var byteArray = ImageWidget.ToBinary(path);
            var result = ImageWidget.ToBase64(byteArray);
            return result;
        }


    }



    /// <summary>
    /// لیست کاربران بوسیله نوع کاربر
    /// </summary>
    /// <param name="userTypeID"></param>
    /// <param name="activate"></param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public ResultViewModel<UserViewModel> SelectAllByUserType(int userTypeID, bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<UserViewModel>();
        try
        {
            IQueryable<User> items;
            Expression<Func<User, bool>> condition = x => x.UserTypeID == userTypeID && (string.IsNullOrWhiteSpace(filter) || x.UserName.Contains(filter) || x.AliasName.Contains(filter) || x.FullName.Contains(filter));
            items = GetAll(activate, condition, pageNumber, pageSize, o => o.FullName, true);
            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0 ?
                new MessageViewModel { Status = Statuses.Success } :
                new MessageViewModel { Status = Statuses.Warning, Message = Messages.RecordNotFoundError610 };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }


    public HttpResponseMessage ExcelAll()
    {
        HttpResponseMessage result;
        var items = GetAll(null).ToList();
        var list = _mapper.Map<List<UserExcelModel>>(items);

        result = _excel.GenerateExcel(list, null, false, "Report", OfficeOpenXml.Table.TableStyles.Medium2, "Sheet1", true, true);

        return result;
    }




    /// <summary>
    /// دریافت تمام کاربران در سمت ادمین
    /// </summary>
    /// <param name="activate"></param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    //public override ResultViewModel<UserViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    //{
    //    var result = new ResultViewModel<UserViewModel>();
    //    try
    //    {
    //        IQueryable<User> items;
    //        Expression<Func<User, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.UserName.Contains(filter) || x.AliasName.Contains(filter) || x.FullName.Contains(filter);
    //        if (!string.IsNullOrWhiteSpace(filter))
    //        {
    //            items = GetAll(activate, condition, pageNumber, pageSize, o => o.Id, true);
    //        }
    //        else
    //        {
    //            items = GetAll(activate, null, pageNumber, pageSize, o => o.Id, true);
    //        }
    //        result.List = MapToViewModel(items);

    //        result.TotalCount = Count(activate, condition);

    //        result.Message = result.TotalCount > 0 ?
    //            new MessageViewModel { Status = Statuses.Success } :
    //            new MessageViewModel { Status = Statuses.Warning, Message = Messages.RecordNotFoundError610 };
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
    //        result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
    //        return result;
    //    }
    //}




    /// <summary>
    /// سازنده گذرواژه تصادفی
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomPassword()
    {
        string result = string.Empty;
        Random rand = new Random();
        int pattern = 1;
        pattern = rand.Next(1, 3);
        if (pattern == 1)
        {
            result = PassordPattern1();
        }
        else if (pattern == 2)
        {
            result = PassordPattern2();
        }
        else if (pattern == 3)
        {
            result = PassordPattern3();
        }
        else
        {
            result = PassordPattern1();
        }
        return result;
    }

    private string PassordPattern1()
    {
        string result = string.Empty;
        int num = 0;
        string chars = "$%#@!*?;:^&";
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "1234567890";
        Random rand = new Random();
        //1,2,3
        for (int i = 1; i <= 3; i++)
        {
            num = rand.Next(0, lowers.Length);
            result = result + lowers[num];
        }
        //4
        num = rand.Next(0, chars.Length);
        result = result + chars[num];
        //5
        num = rand.Next(0, lowers.Length);
        result = result + lowers[num];
        //6
        num = rand.Next(0, uppers.Length);
        result = result + uppers[num];
        //7
        num = rand.Next(0, numbers.Length);
        result = result + numbers[num];
        //8
        num = rand.Next(0, lowers.Length);
        result = result + lowers[num];
        return result;
    }

    private string PassordPattern2()
    {
        string result = string.Empty;
        int num = 0;
        string chars = "$%#@!*?;:^&";
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "1234567890";
        Random rand = new Random();
        //1,2,3,4
        for (int i = 1; i <= 4; i++)
        {
            num = rand.Next(0, lowers.Length);
            result = result + lowers[num];
        }
        //5
        num = rand.Next(0, chars.Length);
        result = result + chars[num];
        //6
        num = rand.Next(0, numbers.Length);
        result = result + numbers[num];
        //7
        num = rand.Next(0, uppers.Length);
        result = result + uppers[num];
        //8
        num = rand.Next(0, lowers.Length);
        result = result + lowers[num];
        return result;
    }

    private string PassordPattern3()
    {
        string result = string.Empty;
        int num = 0;
        string chars = "$%#@!*?;:^&";
        string lowers = "abcdefghijklmnopqrstuvwxyz";
        string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "1234567890";
        Random rand = new Random();
        //1,2,
        for (int i = 1; i <= 2; i++)
        {
            num = rand.Next(0, lowers.Length);
            result = result + lowers[num];
        }
        //3
        num = rand.Next(0, chars.Length);
        result = result + chars[num];
        //4,5
        for (int i = 1; i <= 2; i++)
        {
            num = rand.Next(0, uppers.Length);
            result = result + uppers[num];
        }

        //6,7
        for (int i = 1; i <= 2; i++)
        {
            num = rand.Next(0, numbers.Length);
            result = result + numbers[num];
        }
        //8
        num = rand.Next(0, lowers.Length);
        result = result + lowers[num];
        return result;
    }



    /// <summary>
    /// دریافت کاربر با کد / شناسه ملی
    /// </summary>
    /// <param name="nid"></param>
    /// <returns></returns>
    public UserViewModel SelectUserByNID(string nid)
    {
        UserViewModel result;
        try
        {
            var item = GetAll(null, x => x.NID == nid).FirstOrDefault();
            if (item != null)
            {
                result = MapToViewModel(item);
                return result;
            }
            else
            {
                result = new UserViewModel()
                {
                    ID = 0
                };
            }

        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            return null;
        }
        return result;
    }

    public override UserViewModel? SelectByID(int id)
    {
        UserViewModel result;
        try
        {
            var item = GetAll(null, x => x.Id == id).FirstOrDefault();
            result = MapToViewModel(item);
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            return null;
        }
        return result;
    }

    public override User GetByID(int id)
    {
        User entity;
        var result = GetAll(null, x => x.Id == id);
        entity = result.FirstOrDefault();
        return entity;
    }



    public override void Update(List<User> list, int modifierID)
    {
        var items = GetAll(null, x => list.Select(l => l.Id).Contains(x.Id)).ToList();
        foreach (var item in list)
        {
            SecurityWidget.AntiXSS(item);
            var exist = items.FirstOrDefault(x => x.ID == item.ID);
            if (exist != null)
            {
                item.CreatorID = exist.CreatorID;
                item.CreationDateTime = exist.CreationDateTime;
                _context.Entry(exist).CurrentValues.SetValues(item);
            }
        }
    }

    public override void Update(User entity, int modifierID)
    {
        SecurityWidget.AntiXSS(entity);
        var exist = GetByID(entity.ID);
        if (exist != null)
        {
            //TODO: log
            entity.CreatorID = exist.CreatorID;
            entity.CreationDateTime = exist.CreationDateTime;
            _context.Entry(exist).CurrentValues.SetValues(entity);

        }
    }

    public override void Delete(int id, int modifierID, bool hardDelete = false)
    {
        var user = GetByID(id);
        if (hardDelete == false)
        {
            user.IsDeleted = true;
            Update(user, modifierID);
        }
        else
        {
            _context.Remove(user);
        }
    }

    public override void Delete(List<int> list, int modifierID, bool hardDelete = false)
    {
        var items = GetAll(null, x => list.Contains(x.Id)).ToList();
        if (hardDelete == false)
        {
            foreach (var item in items)
            {
                item.IsDeleted = true;
            }
            Update(items, modifierID);
        }
    }

    public override bool Exist(int id)
    {
        var query = GetAll(null, x => x.Id == id);
        return query.Any();
    }


    public List<ErrorViewModel> ValidationForm(UserViewModel entity)
    {
        var result = new List<ErrorViewModel>();
        if (entity.PersonTypeID == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نوع شخص")
            });
        }
        if (entity.UserTypeID == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نوع کاربر")
            });
        }
        //Required
        if (string.IsNullOrEmpty(entity.NID) || string.IsNullOrWhiteSpace(entity.NID))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه ملی")
            });
        }

        if (entity.PersonTypeID == 1)
        {
            if (string.IsNullOrEmpty(entity.Cellphone) || string.IsNullOrWhiteSpace(entity.Cellphone))
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شماره موبایل")
                });
            }
            if (string.IsNullOrEmpty(entity.RegDate) || string.IsNullOrWhiteSpace(entity.RegDate))
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "تاریخ تولد")
                });
            }
        }

        if (!string.IsNullOrWhiteSpace(entity.Email) && _email.IsEmailValid(entity.Email) == false)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FiledIsNotRegex, "ایمیل")
            });
        }

        if (!string.IsNullOrWhiteSpace(entity.PostalCode) && entity.PostalCode.Length != 10)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "کد پستی", "10")
            });
        }

        return result;
    }

    public override ResultViewModel<UserViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        throw new NotImplementedException();
    }
}