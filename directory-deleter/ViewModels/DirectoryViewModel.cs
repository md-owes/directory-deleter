using CommunityToolkit.Mvvm.Input;
using directory_deleter.Models;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows.Input;

namespace directory_deleter.ViewModels
{
    internal class DirectoryViewModel
    {
        private DirectoryModel _dmodel;
        public string AllLocations { get; set; }
        public string AllFolders { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public DirectoryViewModel()
        {
            _dmodel = new DirectoryModel();
            DeleteCommand = new RelayCommand(DeleteDirectories);
        }

        private async void DeleteDirectories()
        {
            Log.Logger.Debug("Beginning of method DeleteDirectories");
            _dmodel.Folders = AllFolders;
            _dmodel.Locations = AllLocations;
            _dmodel.DeleteFoldersFromDirectories();
            await MainThread.InvokeOnMainThreadAsync(async () =>
            await Application.Current.MainPage.DisplayAlert("Alert", "The specified folders have been deleted", "OK"));
            Log.Logger.Debug("End of method DeleteDirectories");
        }
    }
}
