using System;
using App.Models;

namespace App.Providers
{
    public interface IApiKeyProvider : IDisposable
    {
        ApiKey Create();
        bool Delete(ApiKey apiKey);
        bool Delete(string apiKeyId);
    }
}
