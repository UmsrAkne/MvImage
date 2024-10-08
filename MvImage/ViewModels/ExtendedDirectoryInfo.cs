using System.IO.Abstractions;

namespace MvImage.ViewModels
{
    public class ExtendedDirectoryInfo
    {
        public ExtendedDirectoryInfo(IDirectoryInfo d)
        {
            DirectoryInfo = d;
        }

        public IDirectoryInfo DirectoryInfo { get; set; }

        public char KeyCharacter { get; set; }
    }
}