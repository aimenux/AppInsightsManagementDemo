namespace App
{
    public class Settings
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string ClientSecret { get; set; }
        public string SubscriptionId { get; set; }
        public string AppInsightsApiKeyName { get; set; }
        public string AppInsightsResourceName { get; set; }
        public string AppInsightsResourceGroupName { get; set; }
        public string ResourceUrl => "https://management.core.windows.net";
        public string AuthorityUrl => $"https://login.microsoftonline.com/{TenantId}";
        public string ReadTelemetryRolePath => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/api";
        public string AuthenticateSdkRolePath => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/agentconfig";
        public string WriteAnnotationsRolePath => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/annotations";
    }
}
