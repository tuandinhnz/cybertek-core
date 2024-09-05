using System.Diagnostics;
using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace Cybertek.Settings.CustomConfigurationProviders;

public class AmazonSecretsManagerConfigurationProvider(string region, string secretName) : ConfigurationProvider
{
    private const string Profile = "tuandinh-personal-aws";
    public override void Load()
    {
        string secret = GetSecret();

        Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
    }

    private string GetSecret()
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
        };

        AWSCredentials awsCredentials = LoadSsoCredentials(Profile);
        using var client = new AmazonSecretsManagerClient(awsCredentials, RegionEndpoint.GetBySystemName(region));
        GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;

        string secretString;
        if (response.SecretString != null)
        {
            secretString = response.SecretString;
        }
        else
        {
            MemoryStream memoryStream = response.SecretBinary;
            var reader = new StreamReader(memoryStream);
            secretString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secretString;
    }

    private static SSOAWSCredentials LoadSsoCredentials(string profile)
    {
        var chain = new CredentialProfileStoreChain();
        if (!chain.TryGetAWSCredentials(profile, out AWSCredentials? credentials))
        {
            throw new Exception($"Failed to find the {profile} profile");
        }

        var ssoCredentials = credentials as SSOAWSCredentials;

        ssoCredentials.Options.ClientName = "Cybertek-SSO-App";
        ssoCredentials.Options.SsoVerificationCallback = args =>
        {
            // Launch a browser window that prompts the SSO user to complete an SSO login.
            //  This method is only invoked if the session doesn't already have a valid SSO token.
            // NOTE: Process.Start might not support launching a browser on macOS or Linux. If not,
            //       use an appropriate mechanism on those systems instead.
            Process.Start(new ProcessStartInfo
            {
                FileName = args.VerificationUriComplete,
                UseShellExecute = true
            });
        };

        return ssoCredentials;
    }
}
