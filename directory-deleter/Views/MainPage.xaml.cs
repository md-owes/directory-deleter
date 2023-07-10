using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;

namespace directory_deleter.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void ResetProfile_OnClicked(object sender, EventArgs e)
    {
        var toast = Toast.Make("Profile is reset", CommunityToolkit.Maui.Core.ToastDuration.Long, 30);
        toast.Show();
    }
}