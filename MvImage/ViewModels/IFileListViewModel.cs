using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }

        ObservableCollection<IFileInfo> Files { get; set; }

        IFileInfo SelectedFile { get; set; }

        string PreviewImageFilePath { get; set; }

        IEnumerable<IFileInfo> LoadFiles(string directoryPath);
    }
}