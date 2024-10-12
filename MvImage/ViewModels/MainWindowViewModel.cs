using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using MvImage.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private TextWrapper title = new ();

        [Obsolete("このコンストラクタはデザイン時に利用するためのものです。明示的に呼び出さないでください。")]
        public MainWindowViewModel()
        {
            FileListViewModel = new DummyFileListViewModel();
        }

        public MainWindowViewModel(IContainerProvider containerProvider)
        {
            FileListViewModel = containerProvider.Resolve<IFileListViewModel>();
        }

        public IFileListViewModel FileListViewModel { get; init; }

        public TextWrapper Title { get => title; set => SetProperty(ref title, value); }

        public DelegateCommand MoveFilesWithSameBaseNameCommand => new DelegateCommand(() =>
        {
            var targets = FileListViewModel.Files.Where(f => f.KeyCharacter != default);
            var fs = new FileSystem();
            foreach (var f in targets)
            {
                var dir = FileListViewModel.DirectoryInfoInputArea.DestinationDirectories.FirstOrDefault(d => d.KeyCharacter == f.KeyCharacter);
                if (dir == null)
                {
                    continue;
                }

                MoveFileWithSameBaseName(f.FileInfo.FullName, dir.DirectoryInfo.FullName, fs);
                f.LocationChanged = true;
            }

            FileListViewModel.Files =
                new ObservableCollection<ExtendedFileInfo>(FileListViewModel.Files.Where(f => !f.LocationChanged));
        });

        private void MoveFileWithSameBaseName(string targetFilePath, string destinationDirectoryPath, IFileSystem fileSystem)
        {
            if (string.IsNullOrWhiteSpace(targetFilePath))
            {
                return;
            }

            var parentDirectory = fileSystem.Path.GetDirectoryName(targetFilePath);

            if (parentDirectory == null)
            {
                return;
            }

            FileListViewModel.PreviewImageFilePath = string.Empty;

            var sameNames = fileSystem.Directory.GetFiles(parentDirectory)
                .Where(s => Path.GetFileNameWithoutExtension(s) == Path.GetFileNameWithoutExtension(targetFilePath))
                .ToList();

            foreach (var filePath in sameNames)
            {
                fileSystem.File.Move(filePath, $"{destinationDirectoryPath}\\{Path.GetFileName(filePath)}");
            }
        }
    }
}