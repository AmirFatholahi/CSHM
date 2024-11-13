

using CSHM.Widget.Calendar;
using CSHM.Widget.Config;
using CSHM.Widget.Dapper;
using CSHM.Widget.Elastic;
using CSHM.Widget.Method;
using CSHM.Widget.Stream;
using Dapper;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Razor.Parser.SyntaxTree;

namespace CSHM.Widget.Log;

public class LogWidget : ILogWidget, IDisposable
{
    private readonly StreamWidget _stream;
    private readonly string _path;
    private readonly object _lock;
    private bool _disposed;
    private readonly IHttpContextAccessor _accessor;
    private readonly IDapperWidget _dapper;
    //private readonly IElasticWidget _elastic;
    private readonly LogType _logType;

    public LogWidget(IHttpContextAccessor accessor, IDapperWidget dapper/*, IElasticWidget elastic*/)
    {
        var filePath = ConfigWidget.GetConfigValue<string>("FilePathes:LogFilePath");
        _accessor = accessor;
        _path = Path.Combine(filePath, "logs");
        _stream = new StreamWidget(_path);
        _lock = new object();
        _disposed = false;
        _dapper = dapper;
        //_elastic = elastic;
        _logType = ConfigWidget.GetConfigValue<LogType>("LogConfiguration:LogType");
    }

    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================

    /// <summary>
    /// لاگ استثنائات سیستمی
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="source"></param>
    /// <param name="userID"></param>
    /// <param name="ip"></param>
    /// <param name="logType"></param>
    public async Task ExceptionLog(Exception exception, string? source, int userID = 0)
    {
        List<Task> tasks = new List<Task>();
        var info = GetExceptionInformations(exception);
        try
        {


            if (_logType == LogType.Elastic)
            {
               // tasks.Add(ExceptionLogInElastic(info[0], info[1], info[2], source ?? info[3], userID));
            }
            else if (_logType == LogType.DataBase)
            {
                tasks.Add(ExceptionLogInDatabase(info[0], info[1], info[2], source ?? info[3], userID));
            }
            else if (_logType == LogType.File)
            {
                ExceptionLogInFile(info[0], info[1], info[2], source ?? info[3], userID);
            }
            else if (_logType == LogType.All)
            {
               // tasks.Add(ExceptionLogInElastic(info[0], info[1], info[2], source ?? info[3], userID));
                tasks.Add(ExceptionLogInDatabase(info[0], info[1], info[2], source ?? info[3], userID));
                ExceptionLogInFile(info[0], info[1], info[2], source ?? info[3], userID);
            }
            await Task.WhenAll(tasks);


        }
        catch (Exception ex)
        {
            ExceptionLogInFile(info[0], info[1], info[2], source ?? info[3], userID);
            //await ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0, logType: LogType.File);
            Logger(MethodBase.GetCurrentMethod()?.GetSourceName());
        }
    }


    private async Task ExceptionLogInDatabase(string message, string innerMessage, string code, string? source, int userID)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@System", "ACTS");
        parameters.Add("@Code", code);
        parameters.Add("@Message", message);
        parameters.Add("@InnerMessage", innerMessage);
        parameters.Add("@Source", source);
        parameters.Add("@UserID", userID);
        parameters.Add("@ExceptionDateTime", CalenderWidget.ToJalaliDateTime(DateTime.Now));
        parameters.Add("@CreationDateTime", DateTime.Now);
        await _dapper.CallProcedureAsync<string>("sp_exceptionLog_Insert", parameters);
    }

    private void ExceptionLogInFile(string message, string innerMessage, string code, string? source, int userID)
    {
        var line = $"ACTS-{message}-{innerMessage}-{code}-{source}-{userID}-J{CalenderWidget.ToJalaliDateTime(DateTime.Now)}-{DateTime.Now.ToString("u")}";

        lock (_lock)
        {
            if (!_disposed)
                _stream.Append(DateTime.Now, "exceptions", line);
        }
    }

    private void ExceptionLogInFile(Exception exception, string? source, int userID = 0)
    {
        try
        {
            var info = GetExceptionInformations(exception);
            ExceptionLogInFile(info[0], info[1], info[2], source ?? info[3], userID);
        }
        catch (Exception ex)
        {
            Logger(MethodBase.GetCurrentMethod()?.GetSourceName());
        }
    }

    //private async Task ExceptionLogInElastic(string message, string innerMessage, string code, string? source, int userID)
    //{
    //    var record = new { ID = Guid.NewGuid(), System = "ACTS", User = userID, Message = message, InnerMessage = innerMessage, Code = code, Source = source, ExceptionDateTime = "J" + CalenderWidget.ToJalaliDateTime(DateTime.Now), CreationDateTime = DateTime.Now };
    //    await _elastic.Create<dynamic>(record, "exceptionlogs");
    //}


    private List<string> GetExceptionInformations(Exception exception)
    {
        List<string> result = new List<string>();
        var st = new StackTrace(exception, true);
        // Get the top stack frame
        var frame = st.GetFrame(0);
        // دریافت شماره خط خطا
        var lineNumber = frame?.GetFileLineNumber() ?? 0;
        var messages = FromHierarchy(exception, ex => ex.InnerException).Select(ex => ex.Message);
        var innerMessages = string.Join("////", messages);
        var stackTractObject = new StackTrace();
        var reflectedType = stackTractObject.GetFrame(1)?.GetMethod()?.ReflectedType;
        string source = string.Empty;
        if (reflectedType != null)
        {
            source = reflectedType.Name;
        }
        result.Add($"{exception.Message} In Line {lineNumber}");
        result.Add(innerMessages);
        result.Add(exception.HResult.ToString());
        result.Add(source);
        return result;
    }

    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================Entity Log


    /// <summary>
    /// لاگ اتفاقات موجودیت های بیزینسی
    /// </summary>
    /// <param name="documentID"></param>
    /// <param name="action"></param>
    /// <param name="ip"></param>
    /// <param name="userID"></param>
    /// <param name="logType"></param>
    public async Task EntityLog(string entityName, int entityID, string action, int userID, string changedFields)
    {
        try
        {
            List<Task> tasks = new List<Task>();
            if (_logType == LogType.Elastic)
            {
               // tasks.Add(EntityLogInElastic(entityName, entityID, action, userID, changedFields));
            }
            else if (_logType == LogType.DataBase)
            {
                tasks.Add(EntityLogInDatabase(entityName, entityID, action, userID, changedFields));
            }
            else if (_logType == LogType.File)
            {
                EntityLogInFile(entityName, entityID, action, userID, changedFields);
            }
            else if (_logType == LogType.All)
            {
               // tasks.Add(EntityLogInElastic(entityName, entityID, action, userID, changedFields));
                tasks.Add(EntityLogInDatabase(entityName, entityID, action, userID, changedFields));
                EntityLogInFile(entityName, entityID, action, userID, changedFields);
            }

            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            EntityLogInFile(entityName, entityID, action, userID, changedFields);
            ExceptionLogInFile(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
            Logger(MethodBase.GetCurrentMethod()?.GetSourceName());
        }
    }


    private async Task EntityLogInDatabase(string entityName, int entityID, string action, int userID, string changedFields)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@System", "WLT");
        parameters.Add("@EntityName", entityName);
        parameters.Add("@EntityID", entityID);
        parameters.Add("@Action", action);
        parameters.Add("@UserID", userID);
        parameters.Add("@EventDateTime", CalenderWidget.ToJalaliDateTime(DateTime.Now));
        parameters.Add("@CreationDateTime", DateTime.Now);
        parameters.Add("@ChangedFields", changedFields);
        await _dapper.CallProcedureAsync<string>("sp_entityLog_Insert", parameters);
    }

    private void EntityLogInFile(string entityName, int entityID, string action, int userID, string changedFields)
    {
        var line = $"WLT-{entityName}-{entityID}-{action}-{userID}-J{CalenderWidget.ToJalaliDateTime(DateTime.Now)}-{DateTime.Now.ToString("u")}-{changedFields}";

        lock (_lock)
        {
            if (!_disposed)
                _stream.Append(DateTime.Now, "entities", line);
        }
    }

    //private async Task EntityLogInElastic(string entityName, int entityID, string action, int userID, string changedFields)
    //{
    //    var record = new
    //    {
    //        ID = Guid.NewGuid(),
    //        System = "WLT",
    //        User = userID,
    //        EntityName = entityName,
    //        EntityID = entityID,
    //        Action = action,
    //        EventDateTime = "J" + CalenderWidget.ToJalaliDateTime(DateTime.Now),
    //        CreationDateTime = DateTime.Now,
    //        ChangedFields = changedFields
    //    };
    //    await _elastic.Create<dynamic>(record, "entitylogs");
    //}




    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================User Log

    /// <summary>
    /// لاگ اتفاقات کاربر
    /// </summary>
    /// <param name="documentID"></param>
    /// <param name="action"></param>
    /// <param name="ip"></param>
    /// <param name="actorID"></param>
    /// <param name="logType"></param>
    public async Task UserLog(string system, int userID, string action, string metaData, string ip)
    {
        try
        {
            List<Task> tasks = new List<Task>();
            if (_logType == LogType.Elastic)
            {
                //tasks.Add(UserLogInElastic(system, userID, action, metaData));
            }
            else if (_logType == LogType.DataBase)
            {
                tasks.Add(UserLogInDatabase(system, userID, action, metaData, ip));

            }
            else if (_logType == LogType.File)
            {
                UserLogInFile(system, userID, action, metaData, ip);
            }
            else if (_logType == LogType.All)
            {
                //tasks.Add(UserLogInElastic(system, userID, action, metaData));
                tasks.Add(UserLogInDatabase(system, userID, action, metaData, ip));
                UserLogInFile(system, userID, action, metaData, ip);
            }
            await Task.WhenAll(tasks);

        }
        catch (Exception ex)
        {
            UserLogInFile(system, userID, action, metaData, ip);
            UserLogInFile(system, userID, action, metaData, ip);
            ExceptionLogInFile(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
            Logger(MethodBase.GetCurrentMethod().GetSourceName());
        }
    }

    private async Task UserLogInDatabase(int userID, string action, string metaData)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@System", "WLT");
        parameters.Add("@UserID", userID);
        parameters.Add("@Action", action);
        parameters.Add("@MetaData", metaData);
        parameters.Add("@EventDateTime", CalenderWidget.ToJalaliDateTime(DateTime.Now));
        parameters.Add("@CreationDateTime", DateTime.Now);
        await _dapper.CallProcedureAsync<string>("sp_userLog_Insert", parameters);
    }

    private async Task UserLogInDatabase(string system, int userID, string action, string metaData, string ip)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@System", system);
        parameters.Add("@UserID", userID);
        parameters.Add("@Action", action);
        parameters.Add("@MetaData", metaData);
        parameters.Add("@EventDateTime", CalenderWidget.ToJalaliDateTime(DateTime.Now));
        parameters.Add("@UserIP", ip);
        parameters.Add("@CreationDateTime", DateTime.Now);
        await _dapper.CallProcedureAsync<string>("sp_userLog_Insert", parameters);
    }

    private void UserLogInFile(string system, int userID, string action, string metaData, string ip)
    {
        var line =
               $"{system}-{userID}-{action}-{metaData}-{ip}-J{CalenderWidget.ToJalaliDateTime(DateTime.Now)}-{DateTime.Now.ToString("u")}";

        lock (_lock)
        {
            if (!_disposed)
                _stream.Append(DateTime.Now, "users", line);
        }
    }

    //private async Task UserLogInElastic(string system, int userID, string action, string metaData)
    //{
    //    var record = new { ID = Guid.NewGuid(), System = system, User = userID, Action = action, MetaData = metaData, EventDateTime = "J" + CalenderWidget.ToJalaliDateTime(DateTime.Now), CreationDateTime = DateTime.Now };
    //    await _elastic.Create<dynamic>(record, "userlogs");
    //}



    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // ===================================================================================================================
    // =================================================================================================================== Password Log

    public async Task PasswordLog(int userID, string hashedPassword, string ip)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@userID", userID);
        parameters.Add("@hashedPassword", hashedPassword);
        await _dapper.CallProcedureAsync<string>("sp_passwordLog_insert", parameters);

        await UserLogInDatabase("WLT", userID, "USER_ResetPassword", "", ip);
    }


    public int GetLatestPasswordLog(int userID, string hashedPassword)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@userID", userID);
        parameters.Add("@hashedPassword", hashedPassword);
        var result = _dapper.CallProcedure<int>("sp_getLatestPasswordsOfUser", parameters);
        return result[0];
    }







    /// <summary>
    /// دریافت پیام ناشی از استثنای پیشامد
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public string GetExceptionMessage(Exception ex)
    {
#if DEBUG
        var result = ex.Message;
        return result;
#else
            var result = "استثنای ناشناخته";
            return result;
# endif
    }

    /// <summary>
    /// دریافت پیام ناشی از استثنای پیشامد
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public string GetExceptionHResult(Exception ex)
    {
#if DEBUG
        var result = ex.HResult.ToString();
        return result;
#else
            var result = "کد خطای ناشناخته";
            return result;
# endif
    }
    private void Logger(string source)
    {
        var line =
            $"ACTS-{source}-J{CalenderWidget.ToJalaliDateTime(DateTime.Now)}-{DateTime.Now.ToString("u")}";

        lock (_lock)
        {
            if (!_disposed)
                _stream.Append(DateTime.Now, "logger", line);
        }
    }

    public IEnumerable<TSource> FromHierarchy<TSource>(TSource source, Func<TSource, TSource> nextItem) where TSource : class
    {
        return FromHierarchy(source, nextItem, s => s != null);
    }

    public IEnumerable<TSource> FromHierarchy<TSource>(TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
    {
        for (var current = source; canContinue(current); current = nextItem(current))
        {
            yield return current;
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            if (_disposed)
                return;

            if (_stream != null)
                _stream.Dispose();

            _disposed = true;
        }
    }
}
