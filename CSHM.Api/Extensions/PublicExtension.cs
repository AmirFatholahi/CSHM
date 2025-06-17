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


    //public static string GetVersion()
    //{
    //    Assembly assembly = Assembly.GetExecutingAssembly();
    //    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
    //    string productVersion = fileVersionInfo.ProductVersion;
    //    //var majorVersion = productVersion.Substring(0, productVersion.IndexOf(".", StringComparison.Ordinal));
    //    var majorVersion = GetConfigValue<string>("Version:AppVersion");
    //    string date = productVersion.Substring(productVersion.IndexOf("pre", StringComparison.Ordinal) + 3);
    //    if (date == "")
    //        return "Debuging Mode";
    //    var year = Convert.ToInt32(date.Substring(0, 4));
    //    var month = Convert.ToInt32(date.Substring(4, 2));
    //    var day = Convert.ToInt32(date.Substring(6, 2));
    //    var hour = Convert.ToInt32(date.Substring(8, 2));
    //    var minute = Convert.ToInt32(date.Substring(10, 2));


    //    if (year < 1500)
    //    {
    //        return $"{majorVersion}.{year.ToString().Substring(3, 1)}{month}{day}{hour}{minute} - ({year}/{month}/{day})";
    //    }
    //    else
    //    {
    //        var dt = new DateTime(year, month, day, hour, minute, 0);
    //        var jalaliDate = dt.ToJalaliDate();
    //        return $"{majorVersion}.{jalaliDate.Replace("/", "").Substring(3, jalaliDate.Length - 5)}{hour}{minute} - ({jalaliDate})";
    //    }
    //}

    //public static string GetVersion()
    //{
    //    try
    //    {
    //        Assembly assembly = Assembly.GetExecutingAssembly();
    //        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
    //        string productVersion = fileVersionInfo.ProductVersion ?? string.Empty;

    //        string majorVersion = GetConfigValue<string>("Version:AppVersion") ?? "UnknownVersion";
    //        int preIndex = productVersion.IndexOf("pre", StringComparison.Ordinal);

    //        if (preIndex == -1 || preIndex + 3 >= productVersion.Length)
    //        {
    //            return "Debugging Mode";
    //        }

    //        string date = productVersion.Substring(preIndex + 3);

    //        if (string.IsNullOrEmpty(date) || date.Length < 12 || !date.All(char.IsDigit))
    //        {
    //            return "Debugging Mode";
    //        }

    //        int year = int.Parse(date.Substring(0, 4));
    //        int month = int.Parse(date.Substring(4, 2));
    //        int day = int.Parse(date.Substring(6, 2));
    //        int hour = int.Parse(date.Substring(8, 2));
    //        int minute = int.Parse(date.Substring(10, 2));

    //        if (year < 1500)
    //        {
    //            return $"{majorVersion}.{year % 100:D2}{month:D2}{day:D2}.{hour:D2}{minute:D2}";
    //        }

    //        DateTime dateTime = new DateTime(year, month, day, hour, minute, 0);
    //        string jalaliDate = dateTime.ToJalaliDate(); // Assuming this is your extension method

    //        return $"{majorVersion}.{jalaliDate.Replace("/", "").Substring(3)}{hour:D2}{minute:D2} - ({jalaliDate})";
    //    }
    //    catch
    //    {
    //        return "Debugging Mode";
    //    }
    //}

    public static string GetVersion()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string productVersion = fileVersionInfo.ProductVersion;

        var majorVersion = GetConfigValue<string>("Version:AppVersion");

        // Try to extract the part after "pre" if available
        int preIndex = productVersion.IndexOf("pre", StringComparison.OrdinalIgnoreCase);
        string versionSuffix = preIndex >= 0 ? productVersion.Substring(preIndex + 3) : null;

        // Debugging Mode: when suffix is null or not numeric
        if (string.IsNullOrWhiteSpace(versionSuffix) || !char.IsDigit(versionSuffix[0]))
        {
            return "Debugging Mode";
        }

        try
        {
            // Expecting a format like "202504111330" (12 characters)
            if (versionSuffix.Length < 12)
            {
                return "Invalid Version Format";
            }

            var year = int.Parse(versionSuffix.Substring(0, 4));
            var month = int.Parse(versionSuffix.Substring(4, 2));
            var day = int.Parse(versionSuffix.Substring(6, 2));
            var hour = int.Parse(versionSuffix.Substring(8, 2));
            var minute = int.Parse(versionSuffix.Substring(10, 2));

            if (year < 1500)
            {
                return $"{majorVersion}.{year % 100:D2}{month:D2}{day:D2}.{hour:D2}{minute:D2}";
            }
            else
            {
                var dateTime = new DateTime(year, month, day, hour, minute, 0);
                var jalaliDate = dateTime.ToJalaliDate(); // Your extension method
                string formattedJalali = jalaliDate.Replace("/", "").Substring(3); // Remove year part
                return $"{majorVersion}.{formattedJalali}{hour:D2}{minute:D2} - ({jalaliDate})";
            }
        }
        catch
        {
            return "Invalid Version Format";
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