using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }

        ObservableCollection<IFileInfo> Files { get; set; }

        IEnumerable<IFileInfo> LoadFiles(string directoryPath);
    }
}