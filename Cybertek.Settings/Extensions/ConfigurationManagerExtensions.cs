using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Cybertek.Settings.Extensions
{
    public static class ConfigurationManagerExtensions
    {
        public static ConfigurationManager AddConfigurationFiles(this ConfigurationManager configurationManager, IHostEnvironment env)
        {
            configurationManager
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json");

            return configurationManager;
        }

        public static AppSettingsBase LoadCybertekConfigurations(this ConfigurationManager configurationManager) 
        {
            return configurationManager.GetSection("Cybertek").Get<AppSettingsBase>();
        }
        
        public static TSettings LoadCybertekConfigurations<TSettings>(this ConfigurationManager configurationManager) where TSettings : AppSettingsBase, new()
        {
            return configurationManager.GetSection("Cybertek").Get<TSettings>();
        }
        
    }
}
