using System.IO.Abstractions;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListViewModel : BindableBase, IFileListViewModel
    {
        private DirectoryInfoWrapper currentDirectory;

        public DirectoryInfoWrapper CurrentDirectory
        {
            get => currentDirectory;
            set => SetProperty(ref currentDirectory, value);
        }
    }
}