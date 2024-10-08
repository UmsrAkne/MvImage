using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Abstractions;
using System.Windows;
using MvImage.Models;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DummyFileListViewModel : BindableBase, IFileListViewModel
    {
        private DirectoryInfoWrapper currentDirectory;
        private ObservableCollection<ExtendedFileInfo> files;
        private IFileSystem fileSystem;
        private ExtendedFileInfo selectedFile;

        public DummyFileListViewModel()
        {
            fileSystem = new FileSystem();
            Files = new ObservableCollection<ExtendedFileInfo>
            {
                new (fileSystem.FileInfo.New("test001.png")) { KeyCharacter = 'a', },
                new (fileSystem.FileInfo.New("test002.png")) { KeyCharacter = 'b', },
                new (fileSystem.FileInfo.New("test003.png")),
                new (fileSystem.FileInfo.New("test004.png")),
                new (fileSystem.FileInfo.New("test005.png")),
                new (fileSystem.FileInfo.New("test006.png")),
                new (fileSystem.FileInfo.New("test007.png")),
                new (fileSystem.FileInfo.New("test008.png")),
                new (fileSystem.FileInfo.New("test009.png")),
            };

            DirectoryInfoInputArea.DestinationDirectories = new ObservableCollection<ExtendedDirectoryInfo>
            {
                new (fileSystem.DirectoryInfo.New("testDir1")) { KeyCharacter = 'a', },
                new (fileSystem.DirectoryInfo.New("testDir1")) { KeyCharacter = 'b', },
                new (fileSystem.DirectoryInfo.New("testDir1")) { KeyCharacter = 'c', },
                new (fileSystem.DirectoryInfo.New("testDir1")) { KeyCharacter = '-', },
            };
        }

        public DirectoryInfoWrapper CurrentDirectory
        {
            get => currentDirectory;
            set
            {
                SetProperty(ref currentDirectory, value);
                Files = new ObservableCollection<ExtendedFileInfo>(LoadFiles(currentDirectory.FullName));
            }
        }

        public ObservableCollection<ExtendedFileInfo> Files { get => files; set => SetProperty(ref files, value); }

        public ExtendedFileInfo SelectedFile { get => selectedFile; set => SetProperty(ref selectedFile, value); }

        public string PreviewImageFilePath { get; set; }

        public Visibility PreviewImageVisibility { get; set; }

        public DirectoryInfoInputArea DirectoryInfoInputArea { get; set; } = new (null);

        public IEnumerable<ExtendedFileInfo> LoadFiles(string directoryPath)
        {
            return new List<ExtendedFileInfo>()
            {
                new (fileSystem.FileInfo.New($"{directoryPath}_test001.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test002.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test003.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test004.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test005.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test006.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test007.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test008.png")),
                new (fileSystem.FileInfo.New($"{directoryPath}_test009.png")),
            };
        }
    }
}