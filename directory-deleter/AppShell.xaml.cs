using directory_deleter.Views;

namespace directory_deleter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute($"Root/{nameof(NotePage)}", typeof(NotePage));
	}
}
