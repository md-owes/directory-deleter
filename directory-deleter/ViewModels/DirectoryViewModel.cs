using CommunityToolkit.Mvvm.Input;
using directory_deleter.Models;
using Serilog;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using System.ComponentModel;

namespace directory_deleter.ViewModels
{
    internal class DirectoryViewModel : INotifyPropertyChanged
    {
        private DirectoryModel _directoryModel;
        private ProfileModel _profileModel;
        private string _allLocations;
        private string _allFolders;

        public event PropertyChangedEventHandler PropertyChanged;

        public string AllLocations
        {
            get { return _allLocations; }
            set
            {
                if (value != _allLocations)
                {
                    _allLocations = value;
                    OnPropertyChanged(nameof(AllLocations));
                }
            }
        }
        public string AllFolders
        {
            get { return _allFolders; }
            set
            {
                if (value != _allFolders)
                {
                    _allFolders = value;
                    OnPropertyChanged(nameof(AllFolders));
                }
            }
        }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveProfileCommand { get; private set; }
        public ICommand LoadProfileCommand { get; private set; }
        public ICommand ResetProfileCommand { get; private set; }
        public DirectoryViewModel()
        {

            DeleteCommand = new AsyncRelayCommand(DeleteDirectories);
            SaveProfileCommand = new AsyncRelayCommand(SaveProfile);
            LoadProfileCommand = new AsyncRelayCommand(LoadProfile);
            ResetProfileCommand = new AsyncRelayCommand(ResetProfile);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task DeleteDirectories()
        {
            Log.Logger.Debug("Beginning of method DeleteDirectories");
            string[] lstFolders = AllLocations?.Split('\r');
            string[] lstLocations = AllFolders?.Split('\r');
            if ((lstFolders != null && lstFolders.Length > 0) && (lstLocations != null && lstLocations.Length > 0))
            {
                _directoryModel = new DirectoryModel(lstLocations, lstFolders);
                await _directoryModel.DeleteFoldersFromDirectories();
            }
            Log.Logger.Debug("End of method DeleteDirectories");
        }

        private async Task SaveProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method SaveProfile");
            string[] lstLocations = AllLocations?.Split('\r');
            string[] lstFolders = AllFolders?.Split('\r');
            if ((lstFolders != null && lstFolders.Length > 0) && (lstLocations != null && lstLocations.Length > 0))
            {
                _profileModel = new ProfileModel(lstLocations, lstFolders);
                await _profileModel.SaveProfile(token);
                await Toast.Make($"File is saved").Show(token);
            }

            Log.Logger.Debug("End of method SaveProfile");
        }

        private async Task LoadProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method LoadProfile");
            string[] lstFolders = AllLocations?.Split('\r');
            string[] lstLocations = AllFolders?.Split('\r');
            _profileModel = new ProfileModel(lstLocations, lstFolders);
            ProfileModel _profileLoad = await _profileModel.LoadProfile(token);
            AllFolders = string.Join("\r", _profileLoad.ProfileFolders);
            AllLocations = string.Join("\r", _profileLoad.ProfileLocations);
            Log.Logger.Debug("End of method LoadProfile");
        }

        private async Task ResetProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method ResetProfile");
            AllFolders = AllLocations = "";
            await Toast.Make($"Profile has been reset").Show(token);
            Log.Logger.Debug("End of method ResetProfile");
        }
    }
}
