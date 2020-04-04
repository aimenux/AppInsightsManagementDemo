using System;
using System.Collections.Generic;
using App.Models;
using Microsoft.Azure.Management.ApplicationInsights.Management;
using Microsoft.Azure.Management.ApplicationInsights.Management.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace App.Providers
{
    public sealed class ApiKeyProvider : IApiKeyProvider
    {
        private readonly IOptions<Settings> _options;
        private readonly IApplicationInsightsManagementClient _applicationInsightsManagementClient;

        public ApiKeyProvider(IOptions<Settings> options)
        {
            _options = options;
            var credentials = GetServiceClientCredentials(_options.Value);
            _applicationInsightsManagementClient = new ApplicationInsightsManagementClient(credentials)
            {
                SubscriptionId = _options.Value.SubscriptionId
            };
        }

        public ApiKey Create()
        {
            var settings = _options.Value;

            var aPiKeyRequest = new APIKeyRequest
            {
                Name = settings.AppInsightsApiKeyName,
                LinkedReadProperties = new List<string>
                {
                    settings.ReadTelemetryPermission,
                    settings.AuthenticateSdkPermission
                },
                LinkedWriteProperties = new List<string>
                {
                    settings.WriteAnnotationsPermission
                }
            };

            try
            {
                var apiKey = _applicationInsightsManagementClient.APIKeys.Create(
                    settings.AppInsightsResourceGroupName,
                    settings.AppInsightsResourceName,
                    aPiKeyRequest);
                return new ApiKey(apiKey);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteLine(ex);
                return null;
            }
        }

        public bool Delete(ApiKey apiKey) => Delete(apiKey.Id);

        public bool Delete(string apiKeyId)
        {
            try
            {
                var settings = _options.Value;
                _applicationInsightsManagementClient.APIKeys.Delete(
                    settings.AppInsightsResourceGroupName,
                    settings.AppInsightsResourceName,
                    apiKeyId);
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteLine(ex);
                return false;
            }
        }

        public void Dispose()
        {
            _applicationInsightsManagementClient?.Dispose();
        }

        private static ServiceClientCredentials GetServiceClientCredentials(Settings settings)
        {
            var authenticationContext = new AuthenticationContext(settings.AuthorityUrl);
            var clientCredentials = new ClientCredential(settings.ClientId, settings.ClientSecret);
            var authenticationResult = authenticationContext
                .AcquireTokenAsync(settings.ResourceUrl, clientCredentials)
                .GetAwaiter()
                .GetResult();
            return new TokenCredentials(authenticationResult.AccessToken);
        }
    }
}