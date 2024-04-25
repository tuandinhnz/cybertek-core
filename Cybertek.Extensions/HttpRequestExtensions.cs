using Microsoft.AspNetCore.Http;

namespace Cybertek.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Uri GetFullDisplayUri(this HttpRequest request)
        {
            return new Uri(request.GetFullDisplayUrl());
        }

        private static string GetFullDisplayUrl(this HttpRequest request)
        {
            var forwardedProtocol = request.Headers.GetInsensitive("X-Forwarded-Proto").ToString();
            var forwardedHost = request.Headers.GetInsensitive("X-Forwarded-Host").ToString();
            string protocol = string.IsNullOrWhiteSpace(forwardedProtocol) ? request.Scheme : forwardedProtocol;
            string host = string.IsNullOrWhiteSpace(forwardedHost) ? request.Host.Value : forwardedHost;
            string basePath = request.PathBase.Value;
            string path = request.Path.Value;
            string queryString = request.QueryString.Value;
            return $"{protocol}://{host}{basePath}{path}{queryString}";
        }
    }
}
