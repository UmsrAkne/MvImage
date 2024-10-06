using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Linq;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListViewModel : BindableBase, IFileListViewModel
    {
        private readonly IFileSystem fileSystem = new FileSystem();
        private DirectoryInfoWrapper currentDirectory;

        public DirectoryInfoWrapper CurrentDirectory
        {
            get => currentDirectory;
            set => SetProperty(ref currentDirectory, value);
        }

        public ObservableCollection<IFileInfo> Files { get; set; } = new ();

        public IEnumerable<IFileInfo> LoadFiles(string directoryPath)
        {
            return fileSystem.Directory.GetFiles(directoryPath)
                .Select(p => fileSystem.FileInfo.New(p))
                .Where(f => f.Extension == ".safetensors");
        }
    }
}