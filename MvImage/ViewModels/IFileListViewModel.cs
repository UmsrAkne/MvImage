using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Windows;
using MvImage.Models;
using Prism.Commands;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }

        ObservableCollection<ExtendedFileInfo> Files { get; set; }

        IFileInfo SelectedFile { get; set; }

        string PreviewImageFilePath { get; set; }

        Visibility PreviewImageVisibility { get; set; }

        string DestinationPathText { get; set; }

        DirectoryInfoInputArea DirectoryInfoInputArea { get; set; }

        IEnumerable<ExtendedFileInfo> LoadFiles(string directoryPath);
    }
}