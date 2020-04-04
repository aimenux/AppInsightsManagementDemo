using System;
using System.Text;
using Microsoft.Azure.Management.ApplicationInsights.Management.Models;

namespace App.Models
{
    public class ApiKey
    {
        public string Id { get; }

        public string Key { get; }

        public string Name { get; }

        public string CreatedDate { get; }

        public ApiKey(ApplicationInsightsComponentAPIKey apiKey)
        {
            Name = apiKey.Name;
            Key = apiKey.ApiKey;
            Id = GetApiKeyId(apiKey);
            CreatedDate = apiKey.CreatedDate;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{nameof(Name)}: {Name}");
            builder.AppendLine($"{nameof(Id)}: {Id}");
            builder.AppendLine($"{nameof(Key)}: {Key}");
            builder.Append($"{nameof(CreatedDate)}: {CreatedDate}");
            return builder.ToString();
        }

        private static string GetApiKeyId(ApplicationInsightsComponentAPIKey apiKey)
        {
            var fullId = apiKey.Id;
            var pos = fullId.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
            var id = fullId.Substring(pos, fullId.Length - pos);
            return id;
        }
    }
}
