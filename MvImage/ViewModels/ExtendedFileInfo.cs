using System.IO.Abstractions;

namespace MvImage.ViewModels
{
    public class ExtendedFileInfo
    {
        public ExtendedFileInfo(IFileInfo f)
        {
            FileInfo = f;
        }

        public IFileInfo FileInfo { get; set; }

        public bool IsChecked { get; set; }
    }
}