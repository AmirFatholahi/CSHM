using CSHM.Presentation.Base;
using CSHM.Presentation.OTP;

namespace CSHM.Widget.Redis;

public interface IRedisWidget
{
    bool OTPFree(string otp, string username);

    bool ValidateOTP(string username, string secretKey, string otp);

    List<string> CreateCaptcha(string theme, string oldSessionID);

    bool ValidateCaptcha(string sessionID, string word);

    bool SetUserLock(string username);

    bool IsUserLocked(string username);

    void SetForbiddenToken(string jti);

    bool IsExistForbiddenToken(string jti);



    // ********************************************
    // ********************************************OTP


    MessageViewModel GenerateOTP(OTPViewModel entity);

    MessageViewModel ValidateOTP(OTPViewModel entity);


    // ********************************************************************************************************************
    // ********************************************************************************************************************
    // ********************************************************************************************************************

    T GetData<T>(string key);

    Task<T> GetDataAsync<T>(string key);

    bool SetData<T>(string key, T value, DateTimeOffset expirationTime);

    Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime);

    bool RemoveData(string key);

    Task<bool> RemoveDataAsync(string key);


 




}
