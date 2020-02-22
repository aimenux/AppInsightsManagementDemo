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
        public string ReadTelemetryPermission => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/api";
        public string AuthenticateSdkPermission => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/agentconfig";
        public string WriteAnnotationsPermission => $"/subscriptions/{SubscriptionId}/resourceGroups/{AppInsightsResourceGroupName}/providers/microsoft.insights/components/{AppInsightsResourceName}/annotations";
    }
}
