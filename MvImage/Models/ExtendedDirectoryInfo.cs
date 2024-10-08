using System.IO.Abstractions;

namespace MvImage.Models
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