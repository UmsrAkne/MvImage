using System.IO.Abstractions;

namespace MvImage.ViewModels
{
    public interface IFileListViewModel
    {
        DirectoryInfoWrapper CurrentDirectory { get; set; }
    }
}