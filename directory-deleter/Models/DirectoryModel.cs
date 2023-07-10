using CommunityToolkit.Maui.Alerts;
using Serilog;

namespace directory_deleter.Models
{
    internal class DirectoryModel
    {
        public string[] Locations { get; set; }
        public string[] Folders { get; set; }

        private int _searchCount = 0;
        private int _deleteCount = 0;

        public DirectoryModel(string[] locations, string[] folders)
        {
            Folders = folders;
            Locations = locations;
        }

        public async Task DeleteFoldersFromDirectories()
        {
            foreach (var location in Locations)
            {
                foreach (var folder in Folders)
                {
                    Log.Logger.Debug($"Searching for {folder} in location {location}");
                    SearchAndDelete(location, folder);
                }
            }

            await Toast.Make($"The specified folders have been deleted").Show();
            Log.Logger.Debug($"Total directories searched {_searchCount} and directories deleted {_deleteCount}");
        }

        private void SearchAndDelete(string currentDirectory, string targetFolder)
        {
            string[] subdirs = Directory.GetDirectories(currentDirectory);
            _searchCount++;
            foreach (string directory in subdirs)
            {
                Log.Logger.Debug($"Searching for {targetFolder} subdirectories in location {currentDirectory}");
                if (Path.GetFileName(directory) == targetFolder)
                {
                    Log.Logger.Debug($"Folder {targetFolder} found. Deleting it");
                    Directory.Delete(directory, true); // Delete the directory and all its contents
                    _deleteCount++;
                }
                else
                {
                    SearchAndDelete(directory, targetFolder);
                }
            }
        }
    }
}
