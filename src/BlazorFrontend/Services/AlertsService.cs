using BlazorFrontend.Entities;
using BlazorFrontend.Settings;

namespace BlazorFrontend.Services;

public class AlertsService
{
    private readonly HttpClient _httpClient;

    public AlertsService(IConfiguration configuration)
    {
        var apiSettings = 
            configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>()
            ?? throw new InvalidOperationException("Invalid API configuration");
        
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(apiSettings.Url)
        };
    }

    public async Task<IEnumerable<Alert>?> LoadAlertsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Alert>>("/alerts");
    }
}