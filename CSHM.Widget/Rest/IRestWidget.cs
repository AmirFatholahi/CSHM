using System.Data;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace CSHM.Widget.Rest;

public interface IRestWidget
{
    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر  
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="parameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, string? baseAddress = null, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>       
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    Task<RestResponse> CallRestApiAsync(RestSharp.Method method, string endPoint, ServerName serverName);

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر  
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string? baseAddress = null, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="parameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="parameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر  JSON
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="jsonParameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string jsonParameters,
        string baseAddress, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر  JSON خروجی Datatable
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="jsonParameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    DataTable CallRestApi(RestSharp.Method method, string endPoint, string jsonParameters,
        string baseAddress, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="parameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    Task<RestResponse> CallRestApiAsync<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName);

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, ServerName serverName);

    /// <summary>
    /// برای لاگین در سامانه های داخلی مثل Midware
    /// </summary>
    /// <param name="loginEndPoint">آدرس متد لاگین</param>
    /// <param name="userName">نام کاربری</param>
    /// <param name="passWord">کلمه عبور</param>
    /// <param name="serverName">نام سرور ذخیر شده در appsetting</param>
    /// <returns>توکن</returns>
    TokenViewModel Login(string loginEndPoint, string userName, string passWord, ServerName serverName);


    /// <summary>
    /// متد جنریک لاگین
    /// </summary>
    /// <typeparam name="TLogin">ویو مدل اطلاعات لاگین مانند نام کاربری و کلمه عبور</typeparam>
    /// <typeparam name="TToken">ویو مدل توکن خروجی</typeparam>
    /// <param name="loginEndPoint">آدرس متد ویو مدل</param>
    /// <param name="loginViewModel">ویو مدل حاوی اطلاعات لاگین</param>
    /// <param name="serverAddress">آدرس سرور</param>
    /// <returns>توکن</returns>
    TToken Login<TLogin, TToken>(string loginEndPoint, TLogin loginViewModel, string serverAddress);

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و بدون پاسخ  
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    Task<RestResponse> CallRestApiAsync(RestSharp.Method method, string endPoint, string baseAddress, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <param name="files">فایل ها</param>
    /// <param name="alwaysMultipartFormData">برای ارسال فایل باید True شود</param>
    /// <param name="token">توکن</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, ServerName serverName, ICollection<IFormFile> files, bool alwaysMultipartFormData = false, string? token = null);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="files">فایل ها</param>
    /// <param name="alwaysMultipartFormData">برای ارسال فایل باید True شود</param>
    /// <param name="token">توکن</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string baseAddress, ICollection<IFormFile> files, bool alwaysMultipartFormData = false, string token = null);


    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>ویو مدل برای دانلود فایل</returns>
    FileViewModel CallRestApi(RestSharp.Method method, string endPoint, ServerName serverName);

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <returns>ویو مدل برای دانلود فایل</returns>
    FileViewModel CallRestApi(RestSharp.Method method, string endPoint, string baseAddress, string token = null);




    ////////برای تیکیتوم اضافه شد



    /// <summary>
    /// متد جنریک که مناسب apiهایی می باشد که body آنها از نوع FromForm می باشد.
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="serverName"></param>
    /// <param name="fromform"></param>
    /// <returns></returns>
    TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, bool fromform);

    /// <summary>
    /// متد جنریک که مناسب apiهایی می باشد که body آنها از نوع FromForm می باشد و فایل به همراه دارد
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="serverName"></param>
    /// <param name="files"></param>
    /// <param name="fromform"></param>
    /// <returns></returns>
    TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, ICollection<IFormFile> files, bool fromform);




}