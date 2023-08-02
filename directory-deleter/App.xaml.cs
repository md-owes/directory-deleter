using directory_deleter.Services;
using Serilog;

namespace directory_deleter;

public partial class App : Application
{
    private readonly ISettingsService _service;
    public App(IServiceProvider provider)
    {
        InitializeComponent();
        MainPage = new AppShell();

        _service = provider.GetService<ISettingsService>();
        RegisterLogger();
    }
    private void RegisterLogger()
    {
        if (_service.EnableLogs)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine(FileSystem.AppDataDirectory, "directory-delete.log"),
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }
    }
}
