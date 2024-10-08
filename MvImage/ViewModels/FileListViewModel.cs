using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using MvImage.Models;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListViewModel : BindableBase, IFileListViewModel
    {
        private readonly IFileSystem fileSystem;
        private DirectoryInfoWrapper currentDirectory;
        private ExtendedFileInfo selectedFile;
        private string previewImageFilePath = string.Empty;
        private ObservableCollection<ExtendedFileInfo> files = new ();
        private Visibility previewImageVisibility;

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

        public ExtendedFileInfo SelectedFile
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
                var fileNameWe = Path.GetFileNameWithoutExtension(value.FileInfo.FullName);
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

        public DirectoryInfoInputArea DirectoryInfoInputArea { get; set; } = new (null);

        public IEnumerable<ExtendedFileInfo> LoadFiles(string directoryPath)
        {
            return fileSystem.Directory.GetFiles(directoryPath)
                .Select(p => new ExtendedFileInfo(fileSystem.FileInfo.New(p)))
                .Where(f => f.FileInfo.Extension == ".safetensors");
        }
    }
}