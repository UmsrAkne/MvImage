using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileListViewModel : BindableBase, IFileListViewModel
    {
        private readonly IFileSystem fileSystem;
        private DirectoryInfoWrapper currentDirectory;
        private IFileInfo selectedFile;
        private string previewImageFilePath;
        private ObservableCollection<IFileInfo> files = new ();
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
                Files = new ObservableCollection<IFileInfo>(LoadFiles(currentDirectory.FullName));
            }
        }

        public ObservableCollection<IFileInfo> Files { get => files; set => SetProperty(ref files, value); }

        public IFileInfo SelectedFile
        {
            get => selectedFile;
            set
            {
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

                SetProperty(ref selectedFile, value);
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

        public IEnumerable<IFileInfo> LoadFiles(string directoryPath)
        {
            return fileSystem.Directory.GetFiles(directoryPath)
                .Select(p => fileSystem.FileInfo.New(p))
                .Where(f => f.Extension == ".safetensors");
        }
    }
}