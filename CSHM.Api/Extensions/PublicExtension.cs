using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using CSHM.Widget.Calendar;

namespace CSHM.Api.Extensions;

public static class PublicExtension
{
    private static IConfigurationRoot _config;
    public static IConfigurationRoot GetMyConfig()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //var environment = GetEnviroment();
        if (environment == null)
        {
            environment = "Development";
        }
        if (_config == null)
            _config = new ConfigurationBuilder().AddJsonFile($"appsettings.{environment}.json").Build();
        return _config;

    }
    public static int GetUserID(this ClaimsPrincipal user)
    {
        int result = Convert.ToInt32(user.Claims.SingleOrDefault(c => c.Type == "Id")?.Value);
        return result;
    }

    public static DateTime GetUserFirstLogin(this ClaimsPrincipal user)
    {
        DateTime result = Convert.ToDateTime(user.Claims.SingleOrDefault(c => c.Type == "FirstLogin")?.Value);
        return result;
    }

    public static string GetJTI(this ClaimsPrincipal user)
    {
        string result = user.Claims.SingleOrDefault(c => c.Type == "jti")?.Value??"";
        return result;
    }

    public static int GetUserClaims(this ClaimsPrincipal user, string claimName)
    {
        int result = Convert.ToInt32(user.Claims.SingleOrDefault(c => c.Type == claimName)?.Value);
        return result;
    }
    public static string GetMethodName(this MethodBase methodBase)
    {
        var reflectedType = methodBase.ReflectedType;
        if (reflectedType == null) return null;

        var methodName = $"{reflectedType.Name}:{methodBase.Name}";
        return methodName;

    }


    public static string GetVersion()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string productVersion = fileVersionInfo.ProductVersion;
        //var majorVersion = productVersion.Substring(0, productVersion.IndexOf(".", StringComparison.Ordinal));
        var majorVersion = GetConfigValue<string>("Version:AppVersion");
        string date = productVersion.Substring(productVersion.IndexOf("pre", StringComparison.Ordinal) + 3);
        if (date == "")
            return "Debuging Mode";
        var year = Convert.ToInt32(date.Substring(0, 4));
        var month = Convert.ToInt32(date.Substring(4, 2));
        var day = Convert.ToInt32(date.Substring(6, 2));
        var hour = Convert.ToInt32(date.Substring(8, 2));
        var minute = Convert.ToInt32(date.Substring(10, 2));


        if (year < 1500)
        {
            return $"{majorVersion}.{year.ToString().Substring(3, 1)}{month}{day}{hour}{minute} - ({year}/{month}/{day})";
        }
        else
        {
            var dt = new DateTime(year, month, day, hour, minute, 0);
            var jalaliDate = dt.ToJalaliDate();
            return $"{majorVersion}.{jalaliDate.Replace("/", "").Substring(3, jalaliDate.Length - 5)}{hour}{minute} - ({jalaliDate})";
        }
    }

    public static T GetConfigValue<T>(string configName)
    {
        var myConfig = GetMyConfig();
        var result = myConfig.GetValue<T>(configName);
        return result;
    }
    public static IConfigurationSection? GetConfigSection(string sectionName)
    {
        var myConfig = GetMyConfig();
        var result = myConfig.GetSection(sectionName);
        return result;
    }
}