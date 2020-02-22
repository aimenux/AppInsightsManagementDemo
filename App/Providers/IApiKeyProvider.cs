using System;
using Microsoft.Azure.Management.ApplicationInsights.Management.Models;

namespace App.Providers
{
    public interface IApiKeyProvider : IDisposable
    {
        ApplicationInsightsComponentAPIKey Create();
    }
}
