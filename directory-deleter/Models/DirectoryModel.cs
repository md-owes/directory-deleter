using System.Collections.Concurrent;

namespace directory_deleter.Models
{
    internal class DirectoryModel
    {
        public string Locations { get; set; }
        public string Folders { get; set; }

        public void DeleteFoldersFromDirectories()
        {
            string[] locations = Locations.Split('\r');
            string[] folders = Folders.Split('\r');

            foreach (var location in locations)
            {
                foreach (var folder in folders)
                {
                    string foundFolder = FindFolderParallel(location, folder);

                    if (!string.IsNullOrEmpty(foundFolder))
                    {
                        Console.WriteLine($"Found the folder at: {foundFolder}");
                    }
                    else
                    {
                        Console.WriteLine("Folder not found.");
                    }
                }
            }
        }

        public string FindFolderParallel(string root, string folderToFind)
        {
            ConcurrentQueue<string> dirs = new ConcurrentQueue<string>();
            dirs.Enqueue(root);

            Parallel.ForEach(Partitioner.Create(dirs, EnumerablePartitionerOptions.NoBuffering),
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (dir, loopState) =>
            {
                string currentDir = dir;
                if (new DirectoryInfo(currentDir).Name.Equals(folderToFind, StringComparison.OrdinalIgnoreCase))
                {
                    Directory.Delete(currentDir, true);
                }

                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException)
                {
                    // Catch the exception if we do not have access to a directory and continue with the next one.
                    Console.WriteLine($"No access to {currentDir}. Continuing...");
                    return;
                }

                if (subDirs == null)
                    subDirs = Array.Empty<string>();

                dirs.TryDequeue(out string result);

                foreach (var subDir in subDirs)
                {
                    FindFolderParallel(subDir, folderToFind);
                }
            });

            return dirs.FirstOrDefault(x => new DirectoryInfo(x).Name.Equals(folderToFind, StringComparison.OrdinalIgnoreCase));
        }
    }
}
