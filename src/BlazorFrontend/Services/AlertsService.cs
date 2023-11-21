using BlazorFrontend.Entities;
using BlazorFrontend.Settings;

namespace BlazorFrontend.Services;

public class AlertsService(ApiSettings apiSettings)
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(apiSettings.Url)
    };

    public async Task<IEnumerable<Alert>?> LoadAlertsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Alert>>("/alerts");
    }
}