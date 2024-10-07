using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Linq;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DummyFileListViewModel : BindableBase, IFileListViewModel
    {
        private DirectoryInfoWrapper currentDirectory;
        private ObservableCollection<IFileInfo> files;
        private IFileSystem fileSystem;

        public DummyFileListViewModel()
        {
            fileSystem = new FileSystem();
            Files = new ObservableCollection<IFileInfo>
            {
                fileSystem.FileInfo.New("test001.png"),
                fileSystem.FileInfo.New("test002.png"),
                fileSystem.FileInfo.New("test003.png"),
                fileSystem.FileInfo.New("test004.png"),
                fileSystem.FileInfo.New("test005.png"),
                fileSystem.FileInfo.New("test006.png"),
                fileSystem.FileInfo.New("test007.png"),
                fileSystem.FileInfo.New("test008.png"),
                fileSystem.FileInfo.New("test009.png"),
            };
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

        public ObservableCollection<IFileInfo> Files { get => files; set => SetProperty(ref files, value); }

        public IEnumerable<IFileInfo> LoadFiles(string directoryPath)
        {
            return new List<IFileInfo>()
            {
                fileSystem.FileInfo.New($"{directoryPath}_test001.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test002.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test003.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test004.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test005.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test006.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test007.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test008.png"),
                fileSystem.FileInfo.New($"{directoryPath}_test009.png"),
            };
        }
    }
}