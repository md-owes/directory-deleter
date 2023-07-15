using CommunityToolkit.Mvvm.Input;
using directory_deleter.Models;
using Serilog;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using System.ComponentModel;

namespace directory_deleter.ViewModels
{
    /// <summary>
    /// Represents a view model for the directory model.
    /// </summary>
    internal class DirectoryViewModel : INotifyPropertyChanged
    {
        private DirectoryModel _directoryModel;
        private ProfileModel _profileModel;
        private string _allLocations;
        private string _allFolders;

        public event PropertyChangedEventHandler PropertyChanged;

        public string AllLocations
        {
            get => _allLocations;
            set => SetProperty(ref _allLocations, value, nameof(AllLocations));
        }

        public string AllFolders
        {
            get => _allFolders;
            set => SetProperty(ref _allFolders, value, nameof(AllFolders));
        }

        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteDirectories);
        public ICommand SaveProfileCommand => new AsyncRelayCommand(SaveProfile);
        public ICommand LoadProfileCommand => new AsyncRelayCommand(LoadProfile);
        public ICommand ResetProfileCommand => new AsyncRelayCommand(ResetProfile);

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the value of a property and raises the PropertyChanged event if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected void SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Deletes folders from directories.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        private async Task DeleteDirectories()
        {
            Log.Logger.Debug("Beginning of method DeleteDirectories");
            if (TryGetDirectoriesAndFolders(out string[] lstLocations, out string[] lstFolders))
            {
                _directoryModel = new DirectoryModel(lstLocations, lstFolders);
                await _directoryModel.DeleteFoldersFromDirectories();
            }
            Log.Logger.Debug("End of method DeleteDirectories");
        }

        /// <summary>
        /// Saves the profile model to the file system.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task SaveProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method SaveProfile");
            if (TryGetDirectoriesAndFolders(out string[] lstLocations, out string[] lstFolders))
            {
                _profileModel = new ProfileModel(lstLocations, lstFolders);
                await _profileModel.SaveProfile(token);
                await Toast.Make($"File is saved").Show(token);
            }
            Log.Logger.Debug("End of method SaveProfile");
        }

        /// <summary>
        /// Loads the profile model with the given cancellation token.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// The loaded profile model.
        /// </returns>
        private async Task LoadProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method LoadProfile");
            _profileModel = new ProfileModel(AllLocations?.Split('\r'), AllFolders?.Split('\r'));
            ProfileModel _profileLoad = await _profileModel.LoadProfile(token);

            if (_profileLoad != null)
            {
                AllFolders = string.Join("\r", _profileLoad.ProfileFolders);
                AllLocations = string.Join("\r", _profileLoad.ProfileLocations);
            }
            Log.Logger.Debug("End of method LoadProfile");
        }

        /// <summary>
        /// Resets the profile by setting all folders and locations to empty strings and displaying a toast message.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ResetProfile(CancellationToken token)
        {
            Log.Logger.Debug("Beginning of method ResetProfile");
            AllFolders = AllLocations = "";
            await Toast.Make($"Profile has been reset").Show(token);
            Log.Logger.Debug("End of method ResetProfile");
        }

        /// <summary>
        /// Tries to get the directories and folders from the given locations and folders.
        /// </summary>
        /// <param name="directories">The directories.</param>
        /// <param name="folders">The folders.</param>
        /// <returns>True if the directories and folders were successfully retrieved; otherwise, false.</returns>
        private bool TryGetDirectoriesAndFolders(out string[] directories, out string[] folders)
        {
            directories = AllLocations?.Split('\r');
            folders = AllFolders?.Split('\r');
            return (folders != null && folders.Length > 0) && (directories != null && directories.Length > 0);
        }
    }
}
