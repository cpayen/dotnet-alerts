using BlazorFrontend.Services;
using BlazorFrontend.Settings;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var apiSettings = 
    builder.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>()
    ?? throw new InvalidOperationException("Invalid API configuration");

builder.Services.AddSingleton(apiSettings);
builder.Services.AddSingleton<AlertsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();