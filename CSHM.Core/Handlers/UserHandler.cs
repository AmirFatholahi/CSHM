using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Widget.Captcha;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.OTP;
using CSHM.Widget.Rest;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using CSHM.Widget.Image;
using CSHM.Widget.Config;
using System.Security.Cryptography.X509Certificates;
using CSHM.Widget.Security;
using CSHM.Widget.Convertor;
using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;
using StackExchange.Redis;
using static System.Net.WebRequestMethods;
using CSHM.Widget.Redis;
using CSHM.Data.Context;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Models;
using Xceed.Document.NET;
using CSHM.Widget.Dapper;
using Dapper;
using AutoMapper;
using System.Globalization;
using CSHM.Widget.Calendar;
using CSHM.Widget.Email;
using Nest;
using static Stimulsoft.Report.StiOptions.Export;
using CSHM.Widget.Excel;
using IdentityServer4.Test;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using RestSharp;
using CSHM.Presentation.OTP;
using CSHM.Presentation.Resources;
using CSHM.Presentations.Login;
using CSHM.Presentations.User;

namespace CSHM.Core.Handlers;

public class UserHandler : IUserHandler
{
    private readonly UserManager<User> _userManager;
    private readonly ILogWidget _log;
    private readonly ICaptchaWidget _captcha;
    private readonly IUserService _userService;
    private readonly IUserInRoleService _userInRoleService;
    private readonly IControllerActionService _controllerActionService;
    private readonly IPageService _pageService;
    private readonly IRedisWidget _redis;
    private readonly IDataRepositoryHandler _dataRepository;
    private readonly string _issuer;
    private readonly IDapperWidget _dapper;
    private readonly IMapper _mapper;
    private readonly IEmailWidget _email;
    private readonly IExcelWidget _excel;
    private readonly IRestWidget _rest;

    private readonly DatabaseContext _context;
    private readonly int _accessTokenSetting;
    private readonly int _refreshTokenSetting;
    private readonly int _twoFactorTokenSetting;
    private readonly int _userLockTimeSetting;

    private readonly string _apiKey;



    public UserHandler(ILogWidget log, ICaptchaWidget captcha, DatabaseContext context, UserManager<User> userManager, IUserService userService, IEmailWidget email,
        IControllerActionService controllerActionService, IPageService pageService, 
       IUserInRoleService userInRoleService, IRestWidget rest,
         IDataRepositoryHandler dataRepository, IRedisWidget redis, IDapperWidget dapper, IExcelWidget excel, IMapper mapper)
    {
        _log = log;
        _captcha = captcha;
        _userManager = userManager;
        _userService = userService;
     
        _controllerActionService = controllerActionService;
        _pageService = pageService;
        
        _context = context;
      
        _userInRoleService = userInRoleService;
      
        _dataRepository = dataRepository;
        _redis = redis;
        _excel = excel;
     
        _rest = rest;
        _issuer = ConfigWidget.GetConfigValue<string>("OTPConfiguration:issuer");
        _accessTokenSetting = ConfigWidget.GetConfigValue<int>("TokenConfiguration:AccessTokenTime");
        _refreshTokenSetting = ConfigWidget.GetConfigValue<int>("TokenConfiguration:RefreshTokenTime");
        _twoFactorTokenSetting = ConfigWidget.GetConfigValue<int>("TokenConfiguration:TwoFactoreTokenTime");
        _userLockTimeSetting = ConfigWidget.GetConfigValue<int>("UserLockConfiguration:UserLockTime");
        _apiKey = ConfigWidget.GetConfigValue<string>("HSMConfiguration:ApiKey");
        _dapper = dapper;
        _mapper = mapper;
        _email = email;
   
    }

    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================ Main Methods

    /// <summary>
    /// متد اصلی استعلام اشخاص حقیقی و حقوقی
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public ResultViewModel<UserViewModel> KYC(UserViewModel entity, bool isNeedCaptcha = true)
    {
        ResultViewModel<UserViewModel> result = new ResultViewModel<UserViewModel>();
        var errors = new List<ErrorViewModel>();
        try
        {
            if (isNeedCaptcha == true)
            {
                if (_redis.ValidateCaptcha(entity.CaptchaSessionID, entity.CaptchaWord) == false)
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = "",
                        ErrorMessage = Errors.CaptchaFailed
                    });
                    result.Message = new MessageViewModel()
                    {
                        Status = Statuses.Error,
                        Title = Titles.Error,
                        Message = Errors.InquiryFailed,
                        Errors = errors,
                        ID = 0,
                        Value = ""
                    };
                    return result;
                }
            }

            var exist = _userService.GetAll(null, x => x.NID == entity.NID).FirstOrDefault();
            if (exist != null)
            {
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = "",
                    ErrorMessage = Errors.UserExist
                });
                result.Message = new MessageViewModel()
                {
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Errors.InquiryFailed,
                    Errors = errors,
                    ID = 0,
                    Value = ""
                };
                return result;
            }


            return null;
           
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.ExceptionRaised
            });
            result.Message = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Exception,
                Message = Errors.InquiryFailed,
                Errors = errors,
                ID = -1,
                Value = ""
            };
            return result;
        }
    }





    public MessageViewModel AddUserBridge(UserViewModel entity, int creatorID, string ip)
    {
        MessageViewModel result;
        var errors = new List<ErrorViewModel>();
        try
        {
            if (entity.PersonTypeID == 1 && entity.UserTypeID == 5)
            {
                if (entity.GenderTypeID <= 0 || entity.GenderTypeID > 2)
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = "",
                        ErrorMessage = "جنسیت فرد مشخص نمی باشد"
                    });
                }

                if (string.IsNullOrWhiteSpace(entity.NID) || entity.NID.Length != 10)
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = "",
                        ErrorMessage = Errors.NIDNotValid
                    });
                }

                if (string.IsNullOrWhiteSpace(entity.Cellphone) || entity.Cellphone.Length != 11)
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = "",
                        ErrorMessage = Errors.CellphoneNotValid
                    });
                }

                if (string.IsNullOrWhiteSpace(entity.RegDate))
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = "",
                        ErrorMessage = "تکمیل فیلد تاریخ تولد الزامی می باشد"
                    });
                }

                if (errors.Count > 0)
                {
                    result = new MessageViewModel()
                    {
                        Status = Statuses.Error,
                        Title = Titles.Error,
                        Message = Errors.CreateUserFailed,
                        Errors = errors,
                        ID = 0,
                        Value = ""
                    };
                    return result;
                }

               // result = AddOrUpdateUser(entity, creatorID, ip, true, false);
                return null;
            }
            else
            {
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = "",
                    ErrorMessage = Errors.BridgeRulesNotValid
                });
                result = new MessageViewModel()
                {
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Errors.CreateUserFailed,
                    Errors = errors,
                    ID = 0,
                    Value = ""
                };
                return result;
            }
        }
        catch (Exception ex)
        {
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = _log.GetExceptionMessage(ex)
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Exception,
                Message = Messages.UnknownException,
                Errors = errors,
                ID = -1,
                Value = ""
            };
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            return result;
        }
    }




   

   


    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************


   

    /// <summary>
    /// تولید کننده رفرش توکن
    /// </summary>
    /// <param name="validAudience"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public TokenViewModel GetRefreshToken(int step, string validAudience, int userID, string oldJti, DateTime firstLogin)
    {
        var user = _userService.GetByID(userID);
        //var result = _token.GenerateToken(step, validAudience, user.UserName, oldJti, firstLogin, _accessTokenSetting, _refreshTokenSetting);
        return null;
    }




  

    /// <summary>
    /// دریافت شماره تلفن همراه یک کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public MessageViewModel SelectCellphoneOfUser(int userID)
    {
        MessageViewModel result;
        var user = _userService.GetByID(userID);
        if (user != null)
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Success,
                Title = Titles.API,
                Message = Messages.APIExecuted,
                Errors = null,
                ID = user.ID,
                Value = user.Cellphone.Substring(0, 4) + " xxxx " + user.Cellphone.Substring(8, 3)
            };
            return result;
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.UserNotFound,
                Errors = null,
                ID = -1,
                Value = ""
            };
            return result;
        }
    }



    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //************************************************************************************************************* GAURD

    /// <summary>
    /// دریافت وضعیت گارد برای یک کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public bool GetUserGaurdStatus(int userID)
    {
        var user = _userService.GetByID(userID);
        if (user == null)
        {
            return false;
        }
        else
        {
            if (user.IsGaurd == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// تغییر وضعیت گارد یک کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel SetUserGaurdStatus(int userID, int creatorID)
    {
        MessageViewModel result;
        var user = _userService.GetByID(userID);
        if (user != null)
        {
            string message = "";
            if (user.IsGaurd == true)
            {
                message = $"گارد برای کاربری {user.UserName} غیرفعال شود";
            }
            else
            {
                message = $"گارد برای کاربری {user.UserName} فعال شد";
            }

            user.IsGaurd = !user.IsGaurd;

            _userService.Edit(user, creatorID);

            result = new MessageViewModel()
            {
                Status = Statuses.Success,
                Title = Titles.API,
                Message = message,
                Errors = null,
                ID = 1000,
                Value = ""
            };
            return result;
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.UserNotFound,
                Errors = null,
                ID = -1,
                Value = ""
            };
            return result;
        }
    }


    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //************************************************************************************************************* CMS

  


    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************
    //*************************************************************************************************************

    /// <summary>
    /// دریافت Cliams
    /// </summary>
    /// <param name="user">کاربر</param>
    /// <param name="key">کلید</param>
    /// <returns>لیست از Claim</returns>
    public List<string> GetUserClaims(ClaimsPrincipal user, string key)
    {
        var claims = user.Claims.Where(c => c.Type == key).Select(c => c.Value).ToList();
        return claims;
    }

    /// <summary>
    /// دریافت شناسه کاربر
    /// </summary>
    /// <param name="user">کاربر</param>
    /// <returns>شناسه</returns>
    public int GetUserID(ClaimsPrincipal user)
    {
        return Convert.ToInt32(user.Claims.Single(c => c.Type == "Id").Value);
    }

    /// <summary>
    /// لاگین اصلی به پلتفرم
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="validAudience">مالکیت توکن برای کدامیک از لود بالانسرهاست</param>
    /// <param name="withoutCaptcha">بدون نیاز به کپچا</param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public TokenViewModel? Login(LoginViewModel entity, string validAudience, bool withoutCaptcha, string ip)
    {
        try
        {

            bool isAuthenticated = false;
            int userID = 0;
            //الزام به ورود همراه با کپچا
            if (withoutCaptcha == false)
            {
                if (_redis.ValidateCaptcha(entity.CaptchaSessionID, entity.CaptchaWord) == false)
                {

                    var result = new TokenViewModel
                    {
                        Status = false,
                        Token = "INVALID CAPTCHA"
                    };
                    return result;
                }
            }

            if (_redis.IsUserLocked(entity.Username) == true)
            {
                return new TokenViewModel
                {
                    Status = false,
                    Token = "Locked"
                };
            }


            var user = _userManager.FindByNameAsync(entity.Username).Result;
            if (user != null)
            {

                if ( withoutCaptcha == true)
                {
                    _log.UserLog("CSHM", user.ID, "NEED_CAPTCHA", string.Empty, ip);
                    var result = new TokenViewModel
                    {
                        Status = false,
                        Token = "INVALID CAPTCHA"
                    };
                    return result;
                }

                

                if (user.LockoutEnabled == true)
                {
                    _log.UserLog("CSHM", user.ID, "USER_Locked", string.Empty, ip);
                    return new TokenViewModel
                    {
                        Status = false,
                        Token = "Locked"
                    };
                }
                if (user.IsActive == false)
                {
                    _log.UserLog("CSHM", user.ID, "USER_Deactive", string.Empty, ip);
                    return new TokenViewModel
                    {
                        Status = false,
                        Token = "Deactive"
                    };
                }


                userID = user.ID;


                if (user == null || isAuthenticated == false)
                {
                    if (isAuthenticated == false)
                    {
                        _log.UserLog("CSHM", user != null ? user.ID : 0, "PASS_Wrong", $"UserName:{user.UserName}", ip);

                        //var locked = SetUserLock(user.ID, user.UserName, user.Cellphone, ip);
                    }


                    return new TokenViewModel
                    {
                        Status = false,
                        Token = "Not Authenticated"
                    };
                }

                int step = 0;
                DateTime expireDate;
                if (withoutCaptcha == false)
                {
                    step = 1;
                    expireDate = DateTime.Now.AddMinutes(_twoFactorTokenSetting);

                }
                else
                {
                    step = 2;
                    expireDate = DateTime.Now.AddMinutes(_accessTokenSetting);
                }

                //var token = _token.GenerateToken(step, validAudience, entity.Username, "", DateTime.Now, _accessTokenSetting, _refreshTokenSetting);

                try
                {
                    if (user.IsForced == true)
                    {
                        if (withoutCaptcha == false)
                        {
                            _log.UserLog("CSHM", user.ID, "USER_Force", string.Empty, ip);
                            var result = new TokenViewModel();
                            return result;
                        }
                        else
                        {
                            _log.UserLog("CSHM", user.ID, "USER_Force", string.Empty, ip);
                            var result = new TokenViewModel
                            {
                                Status = false,
                                Token = "Not Authenticated"
                            };
                            return result;
                        }
                    }
                    else
                    {
                        //var result = new TokenViewModel
                        //{
                        //    Status = true,
                        //    Token = token.Token,
                        //    RefreshToken = token.RefreshToken,
                        //    Expiration = expireDate
                        //};

                        var result = new TokenViewModel();
                        _log.UserLog("CSHM", userID, "USER_Login", string.Empty, ip);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
                    return null;
                }
            }
            else
            {
                return new TokenViewModel
                {
                    Status = false,
                    Token = "Not Authenticated"
                };
            }

        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            return null;
        }
    }


    /// <summary>
    /// خروج کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public MessageViewModel Logout(int userID, string ip, string oldJti)
    {
        MessageViewModel result = new MessageViewModel();
        _log.UserLog("CSHM", userID, "USER_Logout", string.Empty, ip);
        _redis.SetForbiddenToken(oldJti);
        return result;
    }


   



    public List<KeyValueViewModel> GetClaimsList(string userName)
    {
        List<KeyValueViewModel> authClaimsList = new List<KeyValueViewModel>();
        var user = _userManager.FindByNameAsync(userName).Result;
        authClaimsList.Add(new KeyValueViewModel { Key = JwtRegisteredClaimNames.Sub, Value = user.UserName });
        authClaimsList.Add(new KeyValueViewModel { Key = "Id", Value = user.ID.ToString() });
        authClaimsList.Add(new KeyValueViewModel { Key = JwtRegisteredClaimNames.Jti, Value = Guid.NewGuid().ToString() });
        return authClaimsList;
    }

   



    /// <summary>
    /// دریافت کیوآر کد کلید محرمانه کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public Base64ViewModel GetQRCodeOfSecretKey(int userID)
    {
        Base64ViewModel result = new Base64ViewModel();
        var user = _userService.GetByID(userID);
        if (user != null)
        {
            result.Image = OTPWidget.GenerateQRCode("Wallet", user.UserName, user.SecretKey);
        }
        return result;
    }

    /// <summary>
    /// جانشینی مجدد کلید محرمانه
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public MessageViewModel GenerateSecretKey(int userID)
    {
        MessageViewModel result;
        var user = _userService.GetByID(userID);
        if (user != null)
        {
            user.SecretKey = OTPWidget.GenerateSecretKey(user.UserName);
            result = _userService.Edit(user, userID);
            if (result.Status == Statuses.Success)
            {
                result = new MessageViewModel()
                {
                    Status = Statuses.Success,
                    Title = Titles.Save,
                    Message = "جانشینی کلید محرمانه با موفقیت انجام شد",
                    Errors = null,
                    ID = 0,
                    Value = ""
                };
            }
            return result;
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = "ایجاد کلید محرمانه جدید با شکست مواجه شد",
                Errors = null,
                ID = 0,
                Value = ""
            };
            return result;
        }
    }



    #region Private Methods

    /// <summary>
    /// متد بازگشتی منو
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private List<MenuViewModel> GenerateMenu(List<NavigationViewModel> list)
    {

        return list.Select(x => new MenuViewModel
        {
            title = x.Title,
            // type = string.IsNullOrWhiteSpace(x.Path)  ? "sub" : "link",
            type = string.IsNullOrWhiteSpace(x.Path) || !string.IsNullOrWhiteSpace(x.Icon) ? "sub" : "link",
            icon = x.Icon,
            path = x.Path,
            active = x.Path == "/dashboard" ? true : false,
            children = x.Children.Any() ? GenerateMenu(x.Children) : new List<MenuViewModel>(),
            Menusub = x.Children.Any() ? true : false
        }).ToList();
    }

    /// <summary>
    /// دریافت فرزندانی که کاربر به  آنها دسترسی دارد
    /// </summary>
    /// <param name="children">لیست فرزندان</param>
    /// <param name="pages">صفحات دارای دسترسی</param>
    /// <returns>لیست از فرزندان</returns>
    private List<NavigationViewModel> GetChild(ICollection<Domain.Page> children, List<Domain.Page> pages)
    {
        return children.Where(child => HasAccessToChild(child, pages))
            .Select(child => new NavigationViewModel
            {
                ID = child.ID,
                Title = child.Title,
                Priority = child.Priority,
                Icon = child.Icon,
                IsMenu = child.IsMenu,
                Name = child.Name,
                ParentID = child.ParentID,
                Path = child.Path,
                IsActive = child.IsActive,

                Children = GetChild(child.Children, pages).OrderBy(x => x.Priority).ToList()

            }).OrderBy(x => x.Priority).ToList();
    }

    /// <summary>
    /// جستجو می کند که این صفحه به کدام یک از فرزدندان خود دسترسی دارد
    /// </summary>
    /// <param name="child">فرزند</param>
    /// <param name="pages">صفحه های دارای دسترسی</param>
    /// <returns>دسترسی دارد یا خیر</returns>
    private bool HasAccessToChild(Domain.Page child, List<Domain.Page> pages)
    {
        foreach (var page in pages)
        {
            if (page.ID == child.ID)
                return true;

            var parent = page.Parent;
            while (parent != null)
            {
                if (parent.ID == child.ID)
                    return true;

                parent = parent.Parent;
            }
        }

        return false;
    }

    /// <summary>
    /// پیمایش درخت صفحات و ساخت لیست فلت
    /// پیاده سازی به صورت بازگشتی
    /// </summary>
    /// <param name="navigationViewModels">درخت صفحات</param>
    /// <returns></returns>
    private List<PageViewModel> GetPagesHierarkey(List<NavigationViewModel> navigationViewModels)
    {
        var result = new List<PageViewModel>();
        foreach (var navigationViewModel in navigationViewModels)
        {
            if (navigationViewModel.Children.Any())
            {
                result.AddRange(GetPagesHierarkey(navigationViewModel.Children));
            }

            result.Add(new PageViewModel
            {
                ID = navigationViewModel.ID,
                ParentID = navigationViewModel.ParentID,
                IsActive = navigationViewModel.IsActive,
                Name = navigationViewModel.Name,
                Title = navigationViewModel.Title,
                Priority = navigationViewModel.Priority,
                Path = navigationViewModel.Path,
                IsMenu = navigationViewModel.IsMenu,
                Icon = navigationViewModel.Icon,
                Breadcrumbs = GetAllParentTitle(navigationViewModel)
            }); ;
        }

        return result;
    }

    private List<string> GetAllParentTitle(NavigationViewModel navigationViewModel)
    {
        List<string> result = new List<string>();
        if (navigationViewModel.ParentID != null && navigationViewModel.ParentID != 0)
        {
            var parent = _pageService.GetByID(navigationViewModel.ParentID ?? 0);
            result.AddRange(GetAllParentTitle(new NavigationViewModel { ID = parent.ID, ParentID = parent.ParentID, Title = parent.Title }));

        }
        result.Add(navigationViewModel.Title);


        return result;
    }


    #endregion



}