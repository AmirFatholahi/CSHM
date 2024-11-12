

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
    private readonly IElasticWidget _elastic;
    private readonly LogType _logType;

    public LogWidget(IHttpContextAccessor accessor, IDapperWidget dapper, IElasticWidget elastic)
    {
        var filePath = ConfigWidget.GetConfigValue<string>("FilePathes:LogFilePath");
        _accessor = accessor;
        _path = Path.Combine(filePath, "logs");
        _stream = new StreamWidget(_path);
        _lock = new object();
        _disposed = false;
        _dapper = dapper;
        _elastic = elastic;
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
                tasks.Add(ExceptionLogInElastic(info[0], info[1], info[2], source ?? info[3], userID));
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
                tasks.Add(ExceptionLogInElastic(info[0], info[1], info[2], source ?? info[3], userID));
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

    private async Task ExceptionLogInElastic(string message, string innerMessage, string code, string? source, int userID)
    {
        var record = new { ID = Guid.NewGuid(), System = "ACTS", User = userID, Message = message, InnerMessage = innerMessage, Code = code, Source = source, ExceptionDateTime = "J" + CalenderWidget.ToJalaliDateTime(DateTime.Now), CreationDateTime = DateTime.Now };
        await _elastic.Create<dynamic>(record, "exceptionlogs");
    }


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
