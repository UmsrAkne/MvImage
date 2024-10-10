using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Windows;
using MvImage.Models;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }

        ObservableCollection<ExtendedFileInfo> Files { get; set; }

        ExtendedFileInfo SelectedFile { get; set; }

        string PreviewImageFilePath { get; set; }

        Visibility PreviewImageVisibility { get; set; }

        DirectoryInfoInputArea DirectoryInfoInputArea { get; set; }

        IEnumerable<ExtendedFileInfo> LoadFiles(string directoryPath);
    }
}