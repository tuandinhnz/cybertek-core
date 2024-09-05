using Microsoft.Extensions.Configuration;

namespace Cybertek.Settings.CustomConfigurationProviders;

public class AmazonSecretsManagerConfigurationSource(string region, string secretName) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AmazonSecretsManagerConfigurationProvider(region, secretName);
    }
}
