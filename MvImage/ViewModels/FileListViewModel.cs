using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using MvImage.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListViewModel : BindableBase, IFileListViewModel
    {
        private readonly IFileSystem fileSystem;
        private DirectoryInfoWrapper currentDirectory;
        private IFileInfo selectedFile;
        private string previewImageFilePath = string.Empty;
        private ObservableCollection<ExtendedFileInfo> files = new ();
        private Visibility previewImageVisibility;
        private ObservableCollection<ExtendedDirectoryInfo> destinationDirectories = new ();
        private string destinationPathText = string.Empty;

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
                Files = new ObservableCollection<ExtendedFileInfo>(LoadFiles(currentDirectory.FullName));
            }
        }

        public ObservableCollection<ExtendedFileInfo> Files { get => files; set => SetProperty(ref files, value); }

        public ObservableCollection<ExtendedDirectoryInfo> DestinationDirectories
        {
            get => destinationDirectories;
            set => SetProperty(ref destinationDirectories, value);
        }

        public IFileInfo SelectedFile
        {
            get => selectedFile;
            set
            {
                SetProperty(ref selectedFile, value);

                if (value == null)
                {
                    PreviewImageVisibility = Visibility.Hidden;
                    return;
                }

                // 同名の画像ファイルが有るかを探す。
                var fileNameWe = Path.GetFileNameWithoutExtension(value.FullName);
                var imgFilePath = fileSystem.Directory.GetFiles(CurrentDirectory.FullName)
                    .Where(p => string.Equals(Path.GetExtension(p), ".png", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault(p => Path.GetFileNameWithoutExtension(p) == fileNameWe);

                if (!string.IsNullOrWhiteSpace(imgFilePath))
                {
                    PreviewImageFilePath = imgFilePath;
                    PreviewImageVisibility = Visibility.Visible;
                }
                else
                {
                    PreviewImageVisibility = Visibility.Hidden;
                }
            }
        }

        public string PreviewImageFilePath
        {
            get => previewImageFilePath;
            set => SetProperty(ref previewImageFilePath, value);
        }

        public Visibility PreviewImageVisibility
        {
            get => previewImageVisibility;
            set => SetProperty(ref previewImageVisibility, value);
        }

        public string DestinationPathText
        {
            get => destinationPathText;
            set => SetProperty(ref destinationPathText, value);
        }

        public DelegateCommand AddDestinationDirectoryCommand => new DelegateCommand(() =>
        {
            AddDestinationDirectory(DestinationPathText);
        });

        public IEnumerable<ExtendedFileInfo> LoadFiles(string directoryPath)
        {
            return fileSystem.Directory.GetFiles(directoryPath)
                .Select(p => new ExtendedFileInfo(fileSystem.FileInfo.New(p)))
                .Where(f => f.FileInfo.Extension == ".safetensors");
        }

        public void AddDestinationDirectory(string directoryPath)
        {
            if (DestinationDirectories.All(d => d.DirectoryInfo.FullName != directoryPath))
            {
                DestinationDirectories.Add(new ExtendedDirectoryInfo(fileSystem.DirectoryInfo.New(directoryPath)));
            }
        }
    }
}