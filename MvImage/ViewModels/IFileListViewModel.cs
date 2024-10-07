using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Windows;
using Prism.Commands;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }

        ObservableCollection<IFileInfo> Files { get; set; }

        IFileInfo SelectedFile { get; set; }

        string PreviewImageFilePath { get; set; }

        Visibility PreviewImageVisibility { get; set; }

        string DestinationPathText { get; set; }

        ObservableCollection<IDirectoryInfo> DestinationDirectories { get; set; }

        public DelegateCommand AddDestinationDirectoryCommand { get; }

        IEnumerable<IFileInfo> LoadFiles(string directoryPath);

        void AddDestinationDirectory(string directoryPath);
    }
}