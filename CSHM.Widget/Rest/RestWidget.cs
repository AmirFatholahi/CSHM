using System.Collections;
using System.Data;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using CSHM.Presentation.Base;
using CSHM.Widget.Config;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.Security;

namespace CSHM.Widget.Rest;

public class RestWidget : IRestWidget
{
    private readonly ILogWidget _log;
    private readonly List<ServerAddressViewModel> _serverAddress;

    public RestWidget(ILogWidget log)
    {
        _log = log;
        var serverUrLsSection = ConfigWidget.GetConfigSection("PublishedServerAddresses");
        _serverAddress = serverUrLsSection.Get<List<ServerAddressViewModel>>();

    }

    #region LOGIN

    /// <summary>
    /// برای لاگین در سامانه های داخلی مثل Midware
    /// </summary>
    /// <param name="loginEndPoint">آدرس متد لاگین</param>
    /// <param name="userName">نام کاربری</param>
    /// <param name="passWord">کلمه عبور</param>
    /// <param name="serverName">نام سرور ذخیر شده در appsetting</param>
    /// <returns>توکن</returns>
    public TokenViewModel Login(string loginEndPoint, string userName, string passWord, ServerName serverName)
    {
        try
        {

            var server = _serverAddress.Single(x => x.ServerName == serverName);
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Username = userName,
               Password = SecurityWidget.DecryptGCM(passWord, SecurityWidget.GetDefaultSecretKey()).PlainText,
            };


            var result = Login<LoginViewModel, TokenViewModel>(loginEndPoint, loginViewModel, server.Address);

            server.Token = result.Token;
            if (serverName == ServerName.BaranNovinext)
            {
                server.Token = result.IdToken;
            }

            server.RefreshToken = result.RefreshToken;
            server.ExpireTime = result.Expiration;
            server.Status = result.Status;
            return result;
        }
        catch (Exception ex)
        {

            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }
    }


    /// <summary>
    /// متد جنریک لاگین
    /// </summary>
    /// <typeparam name="TLogin">ویو مدل اطلاعات لاگین مانند نام کاربری و کلمه عبور</typeparam>
    /// <typeparam name="TToken">ویو مدل توکن خروجی</typeparam>
    /// <param name="loginEndPoint">آدرس متد ویو مدل</param>
    /// <param name="loginViewModel">ویو مدل حاوی اطلاعات لاگین</param>
    /// <param name="serverAddress">آدرس سرور</param>
    /// <returns>توکن</returns>
    public TToken Login<TLogin, TToken>(string loginEndPoint, TLogin loginViewModel, string serverAddress)
    {
        try
        {
            var result = CallRestApi<TLogin, TToken>(RestSharp.Method.Post, loginEndPoint, loginViewModel, serverAddress);
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }
    }

    #endregion

    #region RestAPI Callers

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
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TParams, TResult>(method, endPoint, parameters, serverAddress.Address, serverAddress.Token);
    }
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
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, string? token = null)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TParams, TResult>(method, endPoint, parameters, serverAddress.Address, token);
    }
    /// <summary>
    ///  متد جنریک اجرای رست سرویس با پارامتر  JSON خروجی Datatable
    /// </summary>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="jsonParameters"></param>
    /// <param name="baseAddress"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public DataTable CallRestApi(RestSharp.Method method, string endPoint, string jsonParameters, string baseAddress, string? token = null)
    {
        DataTable result;

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(jsonParameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };
            if (method == RestSharp.Method.Post)
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonParameters, ParameterType.RequestBody);
                //request.AddJsonBody(jsonParameters);
            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);



            if (response.StatusCode == HttpStatusCode.OK)
            {

                result = (DataTable)JsonConvert.DeserializeObject(response.Content, (typeof(DataTable)));


            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }

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
    public Task<RestResponse> CallRestApiAsync<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApiAsync<TParams, TResult>(method, endPoint, parameters, serverAddress.Address, serverAddress.Token);
    }
    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, ServerName serverName)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TResult>(method, endPoint, serverAddress.Address, serverAddress.Token);
    }

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>       
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public Task<RestResponse> CallRestApiAsync(RestSharp.Method method, string endPoint, ServerName serverName)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApiAsync(method, endPoint, serverAddress.Address, serverAddress.Token);
    }


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
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, string? baseAddress, string? token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(parameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };
            if (method == RestSharp.Method.Post)
            {
                if (endPoint.ToLower() == "/token")
                {
                    var strParameter = ReadParameters(parameters);
                    string encodedBody = $"username={strParameter[0]};;;{strParameter[4]}&password={strParameter[1]}&grant_type=password";
                    request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
                    request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
                }
                else
                {
                    request.AddBody(parameters);
                }
            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);



            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }


    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر  
    /// </summary>
    /// <typeparam name="TParams">ویو مدل پارامتر های ارسالی به سرور</typeparam>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="jsonParameters">ویو مدل حاوی پارامتر ها</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string jsonParameters, string baseAddress, string? token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(jsonParameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };
            if (method == RestSharp.Method.Post)
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonParameters, ParameterType.RequestBody);
                //request.AddJsonBody(jsonParameters);
            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);



            if (response.StatusCode == HttpStatusCode.OK)
            {



                //var result2 = JObject.Parse(response.Content);
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }

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
    public Task<RestResponse> CallRestApiAsync<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, string baseAddress, string? token = null)
    {

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(parameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };
            if (method == RestSharp.Method.Post)
            {
                request.AddBody(parameters);
            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            return client.ExecuteAsync(request);

        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }


    }

    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر  
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string? baseAddress, string? token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);

            var response = client.Execute(request);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception(response.ErrorMessage));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }



    /// <summary>
    /// متد جنریک اجرای رست سرویس بدون پارامتر و بدون پاسخ  
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="token">توکن در صورت نیاز</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public Task<RestResponse> CallRestApiAsync(RestSharp.Method method, string endPoint, string baseAddress, string? token = null)
    {


        try
        {
            var client = new RestClient(baseAddress);

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);

            return client.ExecuteAsync(request);
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }
    }


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
    public TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, ServerName serverName, ICollection<IFormFile> files, bool alwaysMultipartFormData = false, string token = null)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TResult>(method, endPoint, serverAddress.Address, files, alwaysMultipartFormData, token);
    }

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// خانم روزبهی:: صرفا برای آپلود
    /// </summary>
    /// <typeparam name="TResult">ویو مدل اطلاعات دریافتی از سرور</typeparam>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <param name="files">فایل ها</param>
    /// <param name="alwaysMultipartFormData">برای ارسال فایل باید True شود</param>
    /// <param name="token">توکن</param>
    /// <returns>مقادیر بازگشتی از API</returns>
    public TResult CallRestApi<TResult>(RestSharp.Method method, string endPoint, string baseAddress, ICollection<IFormFile> files, bool alwaysMultipartFormData = false, string token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json, AlwaysMultipartFormData = alwaysMultipartFormData };
            if (method == RestSharp.Method.Post)
            {
                foreach (var formFile in files)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                    request.AddFile("Files", bytes, formFile.FileName);
                }
            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }


    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="serverName">نام سرور ذخیره شده در AppSetting</param>
    /// <returns>ویو مدل برای دانلود فایل</returns>
    public FileViewModel CallRestApi(RestSharp.Method method, string endPoint, ServerName serverName)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi(method, endPoint, serverAddress.Address, serverAddress.Token);
    }

    /// <summary>
    /// متد جنریک اجرای رست سرویس با پارامتر و اطلاعات ذخیره شده سرور ها در AppSetting
    /// خانم روزبهی:: سرفا برای دانلود
    /// </summary>
    /// <param name="method">نوع متد Post یا Get</param>
    /// <param name="endPoint">آدرس متد</param>
    /// <param name="baseAddress">URL سرور</param>
    /// <returns>ویو مدل برای دانلود فایل</returns>
    public FileViewModel CallRestApi(RestSharp.Method method, string endPoint, string baseAddress, string token = null)
    {
        FileViewModel result;

        try
        {
            var client = new RestClient(baseAddress);

            var request = new RestRequest(endPoint, method) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = new FileViewModel
                {
                    RawBytes = response.RawBytes,
                    ContentType = response.ContentType,
                };
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception(response.ErrorMessage));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }

        return result;
    }



    // ==================================================================================================================================================
    // ==================================================================================================================================================
    // ==================================================================================================================================================
    // ==================================================================================================================================================
    // ==================================================================================================================================================
    /////////////برای تیکیتوم اضافه شد


    /// <summary>
    /// متد جنریک که مناسب apiهایی می باشد که body آنها از نوع FromForm می باشد.
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="serverName"></param>
    /// <param name="fromform">باید در این قسمت true ارسال شود</param>
    /// <returns></returns>
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, bool fromform)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TParams, TResult>(method, endPoint, parameters, serverAddress.Address, fromform, serverAddress.Token);
    }



    /// <summary>
    /// متد جنریک که مناسب apiهایی می باشد که body آنها از نوع FromForm می باشد. و فایل به همراه دارد
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="serverName"></param>
    /// <param name="fromform"></param>
    /// <returns></returns>
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, ServerName serverName, ICollection<IFormFile> files, bool fromform)
    {
        var serverAddress = _serverAddress.Single(x => x.ServerName == serverName);
        if (!CheckAuthentication(serverName))
        {
            var response = Login(serverAddress.LoginEndPoint, serverAddress.UserName, serverAddress.PassWord,
                serverAddress.ServerName);
        }
        return CallRestApi<TParams, TResult>(method, endPoint, parameters, serverAddress.Address, files, fromform, serverAddress.Token);
    }





    /// <summary>
    ///  متد جنریک اجرای رست سرویس با پارامتر  که از Fromform ارسال می شود که فایل به همراه دارد.
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="baseAddress"></param>
    /// <param name="alwaysMultipartFormData"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, string? baseAddress, ICollection<IFormFile> files, bool alwaysMultipartFormData, string? token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(parameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method);

            if (method == RestSharp.Method.Post)
            {
                var strParameter = ReadFromFormParameters(parameters);
                request.AlwaysMultipartFormData = alwaysMultipartFormData;
                foreach (var item in strParameter)
                {
                    request.AddParameter(item.Key.ToString(), item.Value.ToString());
                }

                if (files != null)
                {
                    foreach (var formFile in files)
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            formFile.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        request.AddFile("files", bytes, formFile.FileName);
                    }
                }

            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            throw;
        }

        return result;
    }





    /// <summary>
    ///  متد جنریک اجرای رست سرویس با پارامتر  که از Fromform ارسال می شود
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="method"></param>
    /// <param name="endPoint"></param>
    /// <param name="parameters"></param>
    /// <param name="baseAddress"></param>
    /// <param name="alwaysMultipartFormData"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public TResult CallRestApi<TParams, TResult>(RestSharp.Method method, string endPoint, TParams parameters, string? baseAddress, bool alwaysMultipartFormData, string? token = null)
    {
        TResult result;

        try
        {
            var client = new RestClient(baseAddress);

            if (method == RestSharp.Method.Get)
            {
                var strParameter = ReadParameters(parameters);
                endPoint = $"{endPoint}/{string.Join("/", strParameter)}";
            }

            var request = new RestRequest(endPoint, method);

            if (method == RestSharp.Method.Post)
            {
                var strParameter = ReadFromFormParameters(parameters);
                request.AlwaysMultipartFormData = alwaysMultipartFormData;
                foreach (var item in strParameter)
                {
                    request.AddParameter(item.Key.ToString(), item.Value.ToString());
                }


            }
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = client.Execute(request);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            else
            {
                throw new Exception("Rest Server is not ready", new Exception($"ErrorMessage={response.ErrorMessage ?? "No Error Message"}", new Exception($"StatusCode={response.StatusCode}")));
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            throw;
        }

        return result;
    }




    #endregion


    #region PrivateMethods

    private bool CheckAuthentication(ServerName serverName)
    {
        try
        {
            var server = _serverAddress.SingleOrDefault(x => x.ServerName == serverName);
            if (server == null)
            {
                throw new Exception("سرور پیدا نشد");
            }
            return (string.IsNullOrEmpty(server.UserName) && string.IsNullOrEmpty(server.PassWord) && string.IsNullOrEmpty(server.LoginEndPoint)) || (!string.IsNullOrEmpty(server.Token) && server.ExpireTime >= DateTime.Now);
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            return false;
        }
    }
    private List<string> ReadParameters<TParams>(TParams parameters)
    {
        Type t = parameters.GetType();

        PropertyInfo[] props = t.GetProperties();
        if (t == typeof(string) || t == typeof(int))
        {
            var result = new List<string>();
            result.Add(parameters.ToString());
            return result;
        }

        return props.Select(p => p.GetValue(parameters)?.ToString()).ToList();
    }


    /////////////برای تیکیتوم اضافه شد

    private List<KeyValueViewModel> ReadFromFormParameters<TParams>(TParams parameters)
    {
        Type t = parameters.GetType();
        PropertyInfo[] props = t.GetProperties();
        var result = new List<KeyValueViewModel>();

        if (t == typeof(string) || t == typeof(int))
        {
            result.Add(new KeyValueViewModel { Key = "", Value = parameters.ToString() });
            return result;
        }

        foreach (var prop in props)
        {
            var propValue = prop.GetValue(parameters);

            if (propValue is IEnumerable enumerableValue && !(propValue is string))
            {
                var propDict = new KeyValueViewModel
                {
                    Key = prop.Name,
                    Value = string.Join(",", enumerableValue.Cast<object>())

                };

                result.Add(propDict);
            }
            else
            {
                var propDict = new KeyValueViewModel
                {
                    Key = prop.Name,
                    Value = string.Join(",", propValue?.ToString())

                };

                result.Add(propDict);
            }
        }

        return result;
    }


    #endregion

}