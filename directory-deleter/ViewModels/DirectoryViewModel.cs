using CommunityToolkit.Mvvm.Input;
using directory_deleter.Models;
using System.Collections.ObjectModel;
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

        private void DeleteDirectories()
        {
            _dmodel.Folders = AllFolders;
            _dmodel.Locations = AllLocations;
            _dmodel.DeleteFoldersFromDirectories();
        }
    }
}
