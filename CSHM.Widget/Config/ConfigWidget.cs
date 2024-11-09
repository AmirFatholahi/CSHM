
using Microsoft.Extensions.Configuration;

namespace CSHM.Widget.Config;

public static  class ConfigWidget
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

    public static T GetConfigValue<T>(string configName)
    {
        var myConfig = GetMyConfig();
        var result = myConfig.GetValue<T>(configName);
        return result;
    }

    public static IConfigurationSection GetConfigSection(string sectionName)
    {
        var myConfig = GetMyConfig();
        var result = myConfig.GetSection(sectionName);
        return result;
    }

}