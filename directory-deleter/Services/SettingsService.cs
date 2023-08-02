using Microsoft.Extensions.Configuration;

namespace directory_deleter.Services
{
    public class SettingsService : ISettingsService
    {
        public bool EnableLogs { get; set; }

        public SettingsService()
        {
            EnableLogs = Environment.GetEnvironmentVariable("DD_EnableLogs") == "1";
        }
    }
}
