using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using StackExchange.Redis;
using System.Resources;
using CSHM.Presentation.Base;
using CSHM.Presentation.OTP;
using CSHM.Presentation.Resources;
using CSHM.Widget.Captcha;
using CSHM.Widget.Config;
using CSHM.Widget.OTP;
using CSHM.Widget.Rest;
using static System.Net.WebRequestMethods;

namespace CSHM.Widget.Redis;

public class RedisWidget : IRedisWidget
{
    private readonly IConnectionMultiplexer _connection;
    private IDatabase _database;
    private readonly ICaptchaWidget _captcha;
    private readonly int _userLockTimeSetting;
    private readonly int _userLockSilentSetting;
    private readonly int _userLockCountSetting;

    private readonly int _gatewayExpireTimeSetting;

    private readonly int _blockedOTPTime;
    private readonly int _accessOTPtime;
    private readonly int _blockedOTPCount;
    private readonly string _validOTPKey;
    private readonly string _blockedOTPKey;



    public RedisWidget(IConnectionMultiplexer connection, ICaptchaWidget captcha)
    {
        _connection = connection;
        _captcha = captcha;
        _database = _connection.GetDatabase();
        _userLockTimeSetting = ConfigWidget.GetConfigValue<int>("UserLockConfiguration:UserLockTime");
        _userLockSilentSetting = ConfigWidget.GetConfigValue<int>("UserLockConfiguration:UserLockSilent");
        _userLockCountSetting = ConfigWidget.GetConfigValue<int>("UserLockConfiguration:UserLockCount");

        _gatewayExpireTimeSetting = ConfigWidget.GetConfigValue<int>("GatewayConfiguration:ExpireTime");

        _blockedOTPTime = ConfigWidget.GetConfigValue<int>("OTPConfiguration:BlockedTime");
        _accessOTPtime = ConfigWidget.GetConfigValue<int>("OTPConfiguration:AccessTime");
        _blockedOTPCount = ConfigWidget.GetConfigValue<int>("OTPConfiguration:BlockedCount");
        _validOTPKey = ConfigWidget.GetConfigValue<string>("OTPConfiguration:ValidKey");
        _blockedOTPKey = ConfigWidget.GetConfigValue<string>("OTPConfiguration:BlockedKey");
    }


    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // ********************************************************************************************************************************** OTP

    /// <summary>
    /// بررسی آزاد بودن رمز یکبار مصرف برای  استفاده بعدی
    /// </summary>
    /// <param name="otp"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    public bool OTPFree(string otp, string username)
    {
        bool result = false;
        var exist = GetData<OTPWidgetModel>(otp.ToString());
        if (exist == null || exist.Username != username)
        {
            //چون وجود ندارد پس برای اولین بار آمده و صحیح است
            return true;
        }
        else
        {
            //چون وجود دارد پس برای بار دوم آمده و صحیح نمی باشد.
            return false;
        }
    }


    /// <summary>
    ///  صحت سنجی  اعتبار رمز یکبار مصرف 
    ///  زمان زنده بودن: 2 دقیقه
    ///  زمان مسدود بودن: 5 دقیقه
    /// </summary>
    /// <param name="username"></param>
    /// <param name="secretKey"></param>
    /// <param name="otp"></param>
    /// <returns></returns>
    public bool ValidateOTP(string username, string secretKey, string otp)
    {
        bool result = false;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(otp) || otp.ToString().Length != 6)
        {
            result = false;
            return result;
        }
        if (OTPFree(otp, username) == false)
        {
            result = false;
            return result;
        }
        string key = "OTP-WRONG-" + username;
        double blockedTime = 5;
        double accessTime = 4;
        var exist = GetData<WrongViewModel>(key);
        if (exist != null)
        {
            if (exist.Count > 5)
            {
                exist.Count = exist.Count + 1;
                SetData(key, exist, DateTimeOffset.Now.AddMinutes(blockedTime));
                result = false;
                return result;
            }
            else
            {
                result = OTPWidget.ValidateOTP(secretKey, otp.ToString());
                if (result == false)
                {
                    exist.Count = exist.Count + 1;
                    SetData(key, exist, DateTimeOffset.Now.AddMinutes(blockedTime));
                }
                else
                {
                    OTPWidgetModel entity = new OTPWidgetModel()
                    {
                        Username = username,
                        OTP = otp
                    };
                    SetData(otp.ToString(), entity, DateTimeOffset.Now.AddMinutes(accessTime));
                }
                return result;
            }
        }
        else
        {
            result = OTPWidget.ValidateOTP(secretKey, otp.ToString());
            if (result == false)
            {
                WrongViewModel entity = new WrongViewModel()
                {
                    Username = username,
                    Count = 1
                };
                SetData(key, entity, DateTimeOffset.Now.AddMinutes(blockedTime));
                return result;
            }
            else
            {
                OTPWidgetModel entity = new OTPWidgetModel()
                {
                    Username = username,
                    OTP = otp
                };
                SetData(otp.ToString(), entity, DateTimeOffset.Now.AddMinutes(accessTime));
                return result;
            }

        }
    }


    /// <summary>
    /// اعتبارسنجی رمز پویا از طریق ردیس
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MessageViewModel ValidateOTP(OTPViewModel entity)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        if (string.IsNullOrWhiteSpace(entity.Username) || string.IsNullOrWhiteSpace(entity.SecretKey) || string.IsNullOrWhiteSpace(entity.OTP) || entity.OTP.ToString().Length != 6 || string.IsNullOrWhiteSpace(entity.Action) || string.IsNullOrWhiteSpace(entity.Cellphone))
        {
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.FiledsNotCompleted
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }

        //بررسی اینکه رمز پویای کاربر بلاک نشده باشد
        var blockedRecored = GetData<BlockedOTPViewModel>(_blockedOTPKey);
        if (blockedRecored != null && blockedRecored.Count > _blockedOTPCount)
        {
            SetBlockedOTP(blockedRecored, entity.Username);
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.OTPBlocked
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }

        var exist = GetData<OTPViewModel>(_validOTPKey);
        //اگر رمز پویا از قبل وجود نداشته باشد
        if (exist == null)
        {
            SetBlockedOTP(blockedRecored, entity.Username);
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.OTPNotInRedis
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }

        if (exist.Action == entity.Action && exist.Amount == entity.Amount && exist.Cellphone == entity.Cellphone && exist.UserID == entity.UserID && exist.OTP==entity.OTP &&
            exist.Username == entity.Username && exist.TerminalID == entity.TerminalID && exist.MerchantID == entity.MerchantID && exist.ApiKey == entity.ApiKey)
        {
            var valid = OTPWidget.ValidateOTP(entity.SecretKey, entity.OTP);
            if (valid == false)
            {
                SetBlockedOTP(blockedRecored, entity.Username);
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = "",
                    ErrorMessage = Errors.OTPFailed
                });
                result = new MessageViewModel()
                {
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Errors.OTPFailed,
                    Errors = errors,
                    ID = 0,
                    Value = ""
                };
                return result;

            }
            else
            {
                RemoveData(_validOTPKey);
                result = new MessageViewModel()
                {
                    Status = Statuses.Success,
                    Title = Titles.OTP,
                    Message = Messages.OTPIsValid,
                    Errors = errors,
                    ID = 0,
                    Value = ""
                };
                return result;
            }
           
        }
        else
        {
            SetBlockedOTP(blockedRecored, entity.Username);
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.OTPFieldsNotMatched
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }
    }

    /// <summary>
    /// ایجاد رمز پویا در ردیس
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MessageViewModel GenerateOTP(OTPViewModel entity)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        if (string.IsNullOrWhiteSpace(entity.Username) || string.IsNullOrWhiteSpace(entity.SecretKey)  || string.IsNullOrWhiteSpace(entity.Action) || string.IsNullOrWhiteSpace(entity.Cellphone))
        {
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.FiledsNotCompleted
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPGenerationFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }

        var exist= GetData<OTPViewModel>(_validOTPKey);
        if (exist != null)
        {
            if (exist.LastSentDateTime.AddSeconds(90) > DateTime.Now)
            {
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = "",
                    ErrorMessage = Errors.OTPLastSentError
                });
                result = new MessageViewModel()
                {
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Errors.OTPGenerationFailed,
                    Errors = errors,
                    ID = 0,
                    Value = ""
                };
                return result;
            }
        }
        var otp = OTPWidget.GenerateOTP(entity.SecretKey);
        if (!string.IsNullOrWhiteSpace(otp))
        {
            entity.OTP = otp;
            entity.SecretKey = "";
            entity.LastSentDateTime = DateTime.Now;
            SetData(_validOTPKey, entity, DateTimeOffset.Now.AddMinutes(_accessOTPtime));
            result = new MessageViewModel()
            {
                Status = Statuses.Success,
                Title = Titles.API,
                Message =Messages.OTPGenerated,
                Errors = errors,
                ID = 0,
                Value = otp
            };
            return result;
        }
        else
        {
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = "",
                ErrorMessage = Errors.OTPGenerationFailed
            });
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Errors.OTPGenerationFailed,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }

    }


    private void SetBlockedOTP(BlockedOTPViewModel? entity, string username)
    {
        if (entity == null)
        {
            BlockedOTPViewModel record = new BlockedOTPViewModel()
            {
                Username = username,
                Count = 1
            };
            entity = record;
        }
        else
        {
            entity.Count = entity.Count + 1;
        }
        SetData(_blockedOTPKey, entity, DateTimeOffset.Now.AddMinutes(_blockedOTPTime));
    }

    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // ********************************************************************************************************************************** CAPTCHA

    /// <summary>
    /// سازنده اصلی کپچا
    /// [0]:URL base64
    /// [1]: SessionID
    /// </summary>
    /// <param name="oldSessionID"></param>
    /// <returns></returns>
    public List<string> CreateCaptcha(string theme, string oldSessionID)
    {
        List<string> result = new List<string>();
        //1:remove captcha by sessionID
        RemoveData("CAPTCHA-" + oldSessionID);
        //2:create captcha
        var foreColor = "#ffffff";
        var backColor = "#000000";
        foreColor = ConfigWidget.GetConfigValue<string>($"Theme:{theme}:ForeColor");
        backColor = ConfigWidget.GetConfigValue<string>($"Theme:{theme}:BackColor");

        var captcha = _captcha.CreatCaptcha(foreColor, backColor);
        result.Add(captcha[1]); //captcha url in base64
        var guid = Guid.NewGuid();
        result.Add(guid.ToString());//new sessionID
        CaptchaViewModel entity = new CaptchaViewModel()
        {
            CapthaWord = captcha[0],
            SessionID = guid.ToString(),
            ExpireDateTime = DateTime.Now.AddMinutes(5)
        };
        SetData("CAPTCHA-" + guid.ToString(), entity, DateTimeOffset.Now.AddMinutes(5));
        return result;
    }


    /// <summary>
    /// صحت سنجی اعتبار کپچا
    /// </summary>
    /// <param name="sessionID"></param>
    /// <returns></returns>
    public bool ValidateCaptcha(string sessionID, string word)
    {
        var exist = GetData<CaptchaViewModel>("CAPTCHA-" + sessionID);
        if (exist == null)
        {
            return false;
        }
        else
        {
            RemoveData("CAPTCHA-" + sessionID);
            if (exist.ExpireDateTime < DateTime.Now)
            {
                return false;
            }
            else
            {
                if (word == exist.CapthaWord)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // ********************************************************************************************************************************** FORBIDDEN TOKEN

    /// <summary>
    /// ایجاد توکن ممنوعه
    /// </summary>
    /// <param name="jti"></param>
    public void SetForbiddenToken(string jti)
    {
        if (!string.IsNullOrWhiteSpace(jti))
        {
            SetData("TOKEN-" + jti, jti, DateTimeOffset.Now.AddMinutes(125));
        }

    }


    /// <summary>
    /// بررسی وجود توکن ممنوعه
    /// </summary>
    /// <param name="jti"></param>
    /// <returns></returns>
    public bool IsExistForbiddenToken(string jti)
    {
        var exist = GetData<string>("TOKEN-" + jti);
        if (exist == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // ********************************************************************************************************************************** USER LOCK

    /// <summary>
    /// قفل کننده کاربر
    /// </summary>
    /// <param name="userID"></param>
    public bool SetUserLock(string username)
    {
        BroutForceViewModel record;
        if (!string.IsNullOrWhiteSpace(username))
        {
            var key = "USR-LCK" + username;
            record = GetData<BroutForceViewModel>(key);
            if (record == null)
            {
                record = new BroutForceViewModel()
                {
                    Count = 1,
                    Username = username,
                    IsLocked = false
                };
                SetData(key, record, DateTimeOffset.Now.AddMinutes(_userLockSilentSetting));
            }
            else
            {
                record.Count = record.Count + 1;

                if (record.Count >= _userLockCountSetting)
                {
                    record.IsLocked = true;
                    record.EnableTime = DateTime.Now.AddMinutes(_userLockTimeSetting);
                    SetData(key, record, DateTimeOffset.Now.AddMinutes(_userLockTimeSetting));
                }
                else
                {
                    record.IsLocked = false;
                    SetData(key, record, DateTimeOffset.Now.AddMinutes(_userLockSilentSetting));
                }
            }
            return record.IsLocked;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// بررسی قفل بودن کاربر
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public bool IsUserLocked(string username)
    {
        if (!string.IsNullOrWhiteSpace(username))
        {
            var key = "USR-LCK" + username;
            var record = GetData<BroutForceViewModel>(key);
            if (record == null)
            {
                return false;
            }
            else
            {
                if (record.IsLocked == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
    }




    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // **********************************************************************************************************************************
    // ********************************************************************************************************************************** REDIS CRUD


    public long Count<T>(string key)
    {
        long result = 0;
        result = _database.KeyRefCount(key) ?? 0;
        return result;
    }

    public T? GetData<T>(string key)
    {
        var value = _database.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _database.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);

        return isSet;
    }

    public async Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);

        return isSet;
    }

    public bool RemoveData(string key)
    {
        bool isKeyExist = _database.KeyExists(key);
        if (isKeyExist == true)
        {
            return _database.KeyDelete(key);
        }
        return false;
    }

    public async Task<bool> RemoveDataAsync(string key)
    {
        bool isKeyExist = await _database.KeyExistsAsync(key);
        if (isKeyExist == true)
        {
            return await _database.KeyDeleteAsync(key);
        }
        return false;
    }


}
