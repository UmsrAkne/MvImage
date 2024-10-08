using System.Collections.ObjectModel;
using System.IO.Abstractions;
using MvImage.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DirectoryInfoInputArea : BindableBase
    {
        private readonly IFileSystem fileSystem;
        private string directoryPath;
        private char keyCharacter = '-';

        public DirectoryInfoInputArea(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? new FileSystem();
        }

        public string DirectoryPath
        {
            get => directoryPath;
            set
            {
                if (SetProperty(ref directoryPath, value))
                {
                    RaisePropertyChanged(nameof(AddDestinationDirectoryCommand));
                }
            }
        }

        public char KeyCharacter { get => keyCharacter; set => SetProperty(ref keyCharacter, value); }

        public ObservableCollection<ExtendedDirectoryInfo> DestinationDirectories { get; set; } = new ();

        public DelegateCommand AddDestinationDirectoryCommand => new DelegateCommand(() =>
        {
            if (string.IsNullOrWhiteSpace(DirectoryPath))
            {
                return;
            }

            var d = new ExtendedDirectoryInfo(fileSystem.DirectoryInfo.New(DirectoryPath))
            {
                KeyCharacter = KeyCharacter != '-' ? KeyCharacter : default,
            };

            DestinationDirectories.Add(d);
            DirectoryPath = string.Empty;
            KeyCharacter = '-';
        });
    }
}