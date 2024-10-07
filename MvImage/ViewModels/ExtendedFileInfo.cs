using System.IO.Abstractions;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    public class ExtendedFileInfo : BindableBase
    {
        private bool isChecked;

        public ExtendedFileInfo(IFileInfo f)
        {
            FileInfo = f;
        }

        public IFileInfo FileInfo { get; set; }

        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
    }
}