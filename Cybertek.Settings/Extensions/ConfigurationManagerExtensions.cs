using Cybertek.Settings.CustomConfigurationProviders;
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
                .AddJsonFile($"config.{env.EnvironmentName}.json")
                .AddAmazonSecretsManager("us-west-2", "cybertek-demo-dev-connstring");

            return configurationManager;
        }

        public static IConfigurationSection GetConfigurationsSection(this ConfigurationManager configurationManager) 
        {
            return configurationManager.GetSection("Cybertek");
        }

        private static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder, 
            string region,
            string secretName)
        {
            var configurationSource = new AmazonSecretsManagerConfigurationSource(region, secretName);
            configurationBuilder.Add(configurationSource);
        }
    }
}
