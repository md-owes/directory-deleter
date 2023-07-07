using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Serilog;

namespace directory_deleter.Models
{
    internal class DirectoryModel
    {
        public string Locations { get; set; }
        public string Folders { get; set; }

        private int searchCount = 0;
        private int deleteCount = 0;

        public void DeleteFoldersFromDirectories()
        {
            string[] locations = Locations.Split('\r');
            string[] folders = Folders.Split('\r');

            foreach (var location in locations)
            {
                foreach (var folder in folders)
                {
                    Log.Logger.Debug($"Searching for {folder} in location {location}");
                    SearchAndDelete(location, folder);
                }
            }
            Log.Logger.Debug($"Total directories searched {searchCount} and directories deleted {deleteCount}");
        }

        public void SearchAndDelete(string currentDirectory, string targetFolder)
        {
            string[] subdirs = Directory.GetDirectories(currentDirectory);
            searchCount++;
            foreach (string directory in subdirs)
            {
                Log.Logger.Debug($"Searching for {targetFolder} subdirectories in location {currentDirectory}");
                if (Path.GetFileName(directory) == targetFolder)
                {
                    Log.Logger.Debug($"Folder {targetFolder} found. Deleting it");
                    deleteCount++;
                    Directory.Delete(directory, true); // Delete the directory and all its contents
                }
                else
                {
                    SearchAndDelete(directory, targetFolder);
                }
            }
        }
    }
}
