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
        private readonly IFileSystem fileSystem;
        private DirectoryInfoWrapper currentDirectory;

        public FileListViewModel(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public FileListViewModel()
        {
            fileSystem = new FileSystem();
        }

        public DirectoryInfoWrapper CurrentDirectory
        {
            get => currentDirectory;
            set
            {
                SetProperty(ref currentDirectory, value);
                Files = new ObservableCollection<IFileInfo>(LoadFiles(currentDirectory.FullName));
            }
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