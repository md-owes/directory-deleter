using CommunityToolkit.Maui;
using Serilog;

namespace directory_deleter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
#if DEBUG
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(FileSystem.AppDataDirectory, "directory-delete.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        Console.WriteLine($"App Installed Location is {FileSystem.AppDataDirectory}");
#endif

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
