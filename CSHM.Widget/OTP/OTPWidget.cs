using System.Text;
using Google.Authenticator;

namespace CSHM.Widget.OTP;

public static class OTPWidget
{


    /// <summary>
    /// متد برای ارزیابی درستی رمز وارد شده توسط کاربر
    /// </summary>
    /// <param name="secretKey">کلید کاربر</param>
    /// <param name="otp">کد ورودی</param>
    /// <returns></returns>
    public static bool ValidateOTP(string secretKey, string otp)
    {
        var tfa = new TwoFactorAuthenticator();
        var result = tfa.ValidateTwoFactorPIN(secretKey, otp);
        return result;
    }

    /// <summary>
    /// ایجاد رمز یک بار مصرف
    /// </summary>
    /// <param name="secretKey">کلید کاربر</param>
    /// <returns></returns>
    public static string GenerateOTP(string secretKey)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var result = tfa.GetCurrentPIN(secretKey);
        return result;
    }



    /// <summary>
    /// تولید کننده کلید محرمانه برای کاربر
    /// </summary>
    /// <param name="username">نام کاربری به دلخواه</param>
    /// <returns></returns>
    public static string GenerateSecretKey(string username)
    {
        string result = string.Empty;
        Random oRandom = new Random();
        string rnd = oRandom.Next(100, 9999).ToString();
        result= "CSHM" + username.Substring(0, 5)  + rnd;
        return result;

    }


    /// <summary>
    /// متد تولید QRCode
    /// </summary>
    /// <param name="issuer">صادر کننده</param>
    /// <param name="username">نام کاربری</param>
    /// <param name="secretKey">کلید کاربر</param>
    /// <param name="numberOfPixelsPerQRModule">تعداد پیسکل ها در هر ماژول - حداکثر 10 می تواند باشد</param>
    /// <returns>Url image for QRCode</returns>
    public static string GenerateQRCode(string issuer, string username, string secretKey, int numberOfPixelsPerQRModule = 3)
    {
        string result = string.Empty;
        var tfa = new TwoFactorAuthenticator();
        SetupCode setupInfo = tfa.GenerateSetupCode(issuer, username, secretKey, false, numberOfPixelsPerQRModule);
        result= setupInfo.QrCodeSetupImageUrl;
        return result;
    }

    /// <summary>
    /// متد تولید کلید معادل QRCode
    /// </summary>
    /// <param name="issuer">صادر کننده</param>
    /// <param name="username">نام کاربری</param>
    /// <param name="secretKey">کلید کاربر</param>
    /// <returns>کلید معادل QRCode</returns>
    public static string GenerateManualKey(string issuer, string username, string secretKey)
    {
        string result = string.Empty;
        var tfa = new TwoFactorAuthenticator();
        var setupCode = tfa.GenerateSetupCode(issuer, username, Encoding.UTF8.GetBytes(secretKey), generateQrCode: false);
        result= setupCode.ManualEntryKey;
        return result;
    }

    




}
