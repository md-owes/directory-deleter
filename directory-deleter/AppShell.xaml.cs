namespace directory_deleter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Window.Width = 1024;
        this.Window.Height = 768;
        this.Window.MinimumWidth = 800;
        this.Window.MinimumHeight = 600;
    }
}
