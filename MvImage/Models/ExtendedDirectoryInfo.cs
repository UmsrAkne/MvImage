using System.IO.Abstractions;
using Prism.Mvvm;

namespace MvImage.Models
{
    public class ExtendedDirectoryInfo : BindableBase, IKeyed
    {
        private char keyCharacter;

        public ExtendedDirectoryInfo(IDirectoryInfo d)
        {
            DirectoryInfo = d;
        }

        public IDirectoryInfo DirectoryInfo { get; set; }

        public char KeyCharacter { get => keyCharacter; set => SetProperty(ref keyCharacter, value); }
    }
}