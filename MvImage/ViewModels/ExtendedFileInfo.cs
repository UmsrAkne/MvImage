using System.IO.Abstractions;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    public class ExtendedFileInfo : BindableBase
    {
        private bool isChecked;
        private char keyCharacter;

        public ExtendedFileInfo(IFileInfo f)
        {
            FileInfo = f;
        }

        public IFileInfo FileInfo { get; set; }

        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }

        public char KeyCharacter { get => keyCharacter; set => SetProperty(ref keyCharacter, value); }
    }
}