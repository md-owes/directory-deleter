using CommunityToolkit.Maui.Alerts;
#if (!WINDOWS && !MACCATALYST && !MACOS)
using Microsoft.Toolkit.Uwp.Notifications;
#endif

namespace directory_deleter.Services
{
    class NotificationService
    {
        private static readonly object _lock = new object();
        private static NotificationService _instance;

        public bool SnoozeNotifications { get; set; }

        // Private constructor prevents instantiation from other classes.
        private NotificationService()
        {
            // Initialization code, if any.
        }

        // Public static property to access the instance.
        public static NotificationService Instance
        {
            get
            {
                // Double-check locking to avoid locking every time.
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NotificationService();
                        }
                    }
                }

                return _instance;
            }
        }


        private void ShowWindowsNotification(string title, string message)
        {
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText(title)
                .AddText(message)
                .Show();
        }

        private async Task ShowMauiNotification(string message, CancellationToken token)
        {
            await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long).Show(token);
        }

        public void Show(string message, CancellationToken token)
        {
            if (!SnoozeNotifications)
            {
                //include android and ios symbols below, if in future this software is shipped to android and ios devices
#if (WINDOWS && MACCATALYST && MACOS)
                ShowMauiNotification.(message, token);
#else
                ShowWindowsNotification("Directory Deleter", message);
#endif
            }
        }
    }
}
