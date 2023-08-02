using CommunityToolkit.Maui;
using directory_deleter.Services;
using Microsoft.Extensions.Configuration;

namespace directory_deleter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        builder.Configuration.AddConfiguration(config);

        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddScoped<ISettingsService, SettingsService>();

        return mauiAppBuilder;
    }
}
