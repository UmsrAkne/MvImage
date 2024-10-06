using System.Collections.ObjectModel;
using System.IO.Abstractions;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DummyFileListViewModel : BindableBase, IFileListViewModel
    {
        private DirectoryInfoWrapper currentDirectory;
        private ObservableCollection<IFileInfo> files;

        public DummyFileListViewModel()
        {
            var fileSystem = new FileSystem();
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
            set => SetProperty(ref currentDirectory, value);
        }

        public ObservableCollection<IFileInfo> Files { get => files; set => SetProperty(ref files, value); }
    }
}