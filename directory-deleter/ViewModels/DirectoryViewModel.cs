using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using directory_deleter.Models;
using Serilog;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;

namespace directory_deleter.ViewModels
{
    internal class DirectoryViewModel
    {
        private DirectoryModel _directoryModel;
        private ProfileModel _profileModel;
        public string AllLocations { get; set; }
        public string AllFolders { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveProfileCommand { get; private set; }
        public ICommand LoadProfileCommand { get; private set; }
        public DirectoryViewModel()
        {
            
            DeleteCommand = new AsyncRelayCommand(DeleteDirectories);
            SaveProfileCommand = new AsyncRelayCommand(SaveProfile);
            LoadProfileCommand = new AsyncRelayCommand(LoadProfile);
        }

        private async Task DeleteDirectories()
        {
            Log.Logger.Debug("Beginning of method DeleteDirectories");
            string[] lstFolders = AllLocations.Split('\r'); 
            string[] lstLocations = AllFolders.Split('\r');
            _directoryModel = new DirectoryModel(lstLocations, lstFolders);
            await _directoryModel.DeleteFoldersFromDirectories();
            Log.Logger.Debug("End of method DeleteDirectories");
        }

        private async Task SaveProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method SaveProfile");
            string[] lstLocations = AllLocations.Split('\r'); 
            string[] lstFolders = AllFolders.Split('\r');
            _profileModel = new ProfileModel(lstLocations, lstFolders);
            await _profileModel.SaveProfile(token);
            await Toast.Make($"File is saved").Show(token);
            Log.Logger.Debug("End of method SaveProfile");
        }

        private async Task LoadProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method LoadProfile");
            string[] lstFolders = AllLocations.Split('\r'); 
            string[] lstLocations = AllFolders.Split('\r');
            _profileModel = new ProfileModel(lstLocations, lstFolders);
            await _profileModel.LoadProfile(token);
            Log.Logger.Debug("End of method LoadProfile");
        }
    }
}
