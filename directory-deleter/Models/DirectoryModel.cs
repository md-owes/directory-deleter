using CommunityToolkit.Maui.Alerts;
using Serilog;

namespace directory_deleter.Models
{
    /// <summary>
    /// Represents a directory model.
    /// </summary>
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

        /// <summary>
        /// Searches and deletes folders from specified locations.
        /// </summary>
        /// <returns>
        /// Toast notification when folders have been deleted.
        /// </returns>
        public async Task DeleteFoldersFromDirectories()
        {
            foreach (var location in Locations)
            {
                foreach (var folder in Folders)
                {
                    Log.Logger.Debug($"Searching for {folder} in location {location}");
                    await SearchAndDeleteAsync(location, folder);
                }
            }

            await Toast.Make($"The specified folders have been deleted").Show();
            Log.Logger.Debug($"Total directories searched {_searchCount} and directories deleted {_deleteCount}");
        }

        /// <summary>
        /// Searches for and deletes a specified folder in the given directory and all its subdirectories.
        /// </summary>
        /// <param name="currentDirectory">The directory to search in.</param>
        /// <param name="targetFolder">The folder to search for and delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task SearchAndDeleteAsync(string currentDirectory, string targetFolder)
        {
            await Task.Run(async () =>
            {
                string[] subdirs = Directory.GetDirectories(currentDirectory);
                _searchCount++;
                foreach (string directory in subdirs)
                {
                    Log.Logger.Debug($"Searching for {targetFolder} subdirectories in location {currentDirectory}");
                    if (Path.GetFileName(directory) == targetFolder)
                    {
                        await DeleteDirectoryAsync(directory);
                        _deleteCount++;
                    }
                    else
                    {
                        await SearchAndDeleteAsync(directory, targetFolder);
                    }
                }
            });
        }

        /// <summary>
        /// Asynchronously deletes a directory and all its contents.
        /// </summary>
        /// <param name="directory">The directory to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task DeleteDirectoryAsync(string directory)
        {
            await Task.Run(() =>
            {
                Log.Logger.Debug($"Deleting directory: {directory}");
                Directory.Delete(directory, true); // Delete the directory and all its contents
            });
        }
    }
}
